using System;
using System.Net.Http;
using Fusillade;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Simit.Core.Api.Infrastructure
{
    public class ApiService<T> : IApiService<T>
    {
        readonly Func<HttpMessageHandler, T> _createClient;
        public static RefitSettings GetNewtonsoftJsonRefitSettings() => new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));

        public ApiService(string apiBaseAddress)
        {
            _createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(apiBaseAddress)//,
                    //Timeout = TimeSpan.FromSeconds(5)
                };

                return RestService.For<T>(client, GetNewtonsoftJsonRefitSettings());
            };
        }

        private T Background
        {
            get
            {
                return new Lazy<T>(() => _createClient(
                    new RateLimitedHttpMessageHandler( new HttpClientHandler(), Priority.Background))).Value;
            }
        }

        private T UserInitiated
        {
            get
            {
                return new Lazy<T>(() => _createClient(
                    new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.UserInitiated))).Value;
            }
        }

        private T Speculative
        {
            get
            {
                return new Lazy<T>(() => _createClient(
                    new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Speculative))).Value;
            }
        }

        public T GetApi(Priority priority)
        {
            switch (priority)
            {
                case Priority.Background:
                    return Background;
                case Priority.UserInitiated:
                    return UserInitiated;
                case Priority.Speculative:
                    return Speculative;
                default:
                    return UserInitiated;
            }
        }

    }

}
