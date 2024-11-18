using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Simit.Core.Models;

namespace Simit.Core.Api
{
    [Headers("Content-Type: application/json")]
    public interface ISimitApi
    {
        [Get("/simit/comunicazioni")]
        Task<ApiResponse<List<Comunicazioni>>> GetComunicazioni();

        [Get("/simit/media-v2")]
        Task<ApiResponse<List<Call4Talk>>> GetMedia();

        [Get("/simit/programma-v2")]
        Task<ApiResponse<List<Programma>>> GetProgramma();
        
        [Get("/simit/faculty")]
        Task<ApiResponse<List<string>>> GetFaculty();

        [Post("/simit/login")]
        [Headers("Content-Type: application/json")]
        Task<ApiResponse<string>> Login([Body(BodySerializationMethod.Serialized)] LoginRequest loginRequest);
    }
}
