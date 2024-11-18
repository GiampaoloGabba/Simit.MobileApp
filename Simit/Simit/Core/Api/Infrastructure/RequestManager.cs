using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Polly;
using Refit;
using Xamarin.Essentials;

namespace Simit.Core.Api.Infrastructure
{
    public abstract class RequestManager
    {
        public bool IsConnected { get; set; }
        readonly Dictionary<int, CancellationTokenSource> _runningTasks = new Dictionary<int, CancellationTokenSource>();

        protected RequestManager()
        {
            IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public virtual TData GetFromCache<TData>(string cacheKey, out bool isExpired)
        {
            try
            {
                isExpired = Barrel.Current.IsExpired(cacheKey);
                return Barrel.Current.Get<TData>(cacheKey);
            }
            catch (Exception e)
            {
                isExpired = true;
                return default(TData);
            }
        }

        public virtual void SaveToCache<TData>(string cacheKey, TData data, int days)
        {
            if (data == null)
                EmptyCache(cacheKey);
            else
                Barrel.Current.Add(cacheKey, data, TimeSpan.FromDays(days));
        }

        public virtual void EmptyCache(string cacheKey)
        {
            Barrel.Current.Empty(cacheKey);
        }

        protected virtual void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess == NetworkAccess.Internet;

            if (!IsConnected)
            {
                // Cancel All Running Task
                var items = _runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    _runningTasks.Remove(item.Key);
                }
            }
        }

        protected virtual async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
        {
            var response = await Policy
                .Handle<WebException>()
                .Or<HttpRequestException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync
                (
                    retryCount: 2,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () => await task.ConfigureAwait(false))
                .ConfigureAwait(false);

            return response;
        }

        protected virtual void UpdateResponseWithCache<TData>(ref ApApiResponse<TData> response, string cacheKey)
        {
            if (cacheKey != "")
            {
                response.Content      = GetFromCache<TData>(cacheKey, out bool isExpired);
                response.FromCache    = true;
                response.CacheExpired = isExpired;
            }
            else
            {
                response.Content = default(TData);
            }
        }

        protected virtual async Task<ApApiResponse<TData>> BaseRequest<TData>(Task<ApiResponse<TData>> request, bool onlyFromCache = false, string cacheKey = "", int expirationDays = 1 )
        {
            var response = new ApApiResponse<TData>();

            if (onlyFromCache)
            {
                response.IsSuccessStatusCode = true;
                response.StatusCode   = HttpStatusCode.NotModified;
                UpdateResponseWithCache(ref response, cacheKey);

                return response;
            }

            if (!IsConnected)
            {
                response.StatusCode   = HttpStatusCode.RequestTimeout;
                response.ErrorContent = "There's not a network connection";

                UpdateResponseWithCache(ref response, cacheKey);

                return response;
            }

            var cts  = new CancellationTokenSource();
            var task = RemoteRequestAsync(request);
            _runningTasks.Add(task.Id, cts);

            try
            {
                var ris = await task.ConfigureAwait(false);

                response.ReasonPhrase        = ris.ReasonPhrase;
                response.IsSuccessStatusCode = ris.IsSuccessStatusCode || ris.StatusCode == HttpStatusCode.NotFound;
                response.StatusCode          = ris.StatusCode;
                response.Content             = ris.Content;

                if (ris.Error != null)
                {
                    response.ErrorContent = ris.Error.Content?
                        .Replace("{","")
                        .Replace("}", "")
                        .Replace("\"message\":", "")
                        .TrimStart('"')
                        .TrimEnd('"');
                }

                if (cacheKey != "")
                    SaveToCache(cacheKey, ris.Content, expirationDays);
            }
            catch (ApiException ex)
            {
                response.ReasonPhrase = ex.ReasonPhrase;
                response.StatusCode   = ex.StatusCode;
                response.ErrorContent = ex.ReasonPhrase;

                UpdateResponseWithCache(ref response, cacheKey);
            }
            catch (WebException ex)
            {
                response.ReasonPhrase = ex.Message;
                response.StatusCode   = HttpStatusCode.InternalServerError;
                response.ErrorContent = ex.Message;

                UpdateResponseWithCache(ref response, cacheKey);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorContent = ex.Message;

                UpdateResponseWithCache(ref response, cacheKey);
            }

            _runningTasks.Remove(task.Id);
            return response;
        }
    }
}
