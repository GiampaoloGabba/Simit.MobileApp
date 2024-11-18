using System.Net;

namespace Simit.Core.Api
{
    public class ApApiResponse<T>
    {
        public bool FromCache { get; set; }
        public bool CacheExpired { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string ErrorContent { get; set; }
        public T Content { get; set; }
    }
}
