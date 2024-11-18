using System.Collections.Generic;
using System.Threading.Tasks;
using Fusillade;
using Simit.Core.Api.Infrastructure;
using Simit.Core.Models;

namespace Simit.Core.Api
{
    public class SimitApiManager : RequestManager, ISimitApiManager
    {
        readonly IApiService<ISimitApi> _simitApiService;
        public SimitApiManager(IApiService<ISimitApi> simitApiService)
        {
            _simitApiService = simitApiService;
        }

        public async Task<ApApiResponse<List<Comunicazioni>>> GetComunicazioni(Priority priority = Priority.Background, bool onlyFromCache = false)
        {
            return await BaseRequest(_simitApiService.GetApi(priority).GetComunicazioni(), onlyFromCache, nameof(Comunicazioni)).ConfigureAwait(false);
        }

        public async Task<ApApiResponse<List<Call4Talk>>> GetMedia(Priority priority = Priority.Background, bool onlyFromCache = false)
        {
            return await BaseRequest(_simitApiService.GetApi(priority).GetMedia(), onlyFromCache, nameof(Call4Talk)).ConfigureAwait(false);
        }

        public async Task<ApApiResponse<List<Programma>>> GetProgramma(Priority priority, bool onlyFromCache = false)
        {
            return await BaseRequest(_simitApiService.GetApi(priority).GetProgramma(), onlyFromCache, nameof(Programma)).ConfigureAwait(false);
        }

        public async Task<ApApiResponse<string>> Login(LoginRequest loginRequest)
        {
	        return await BaseRequest(_simitApiService.GetApi(Priority.UserInitiated).Login(loginRequest), false, nameof(Login)).ConfigureAwait(false);
        }
        
        public async Task<ApApiResponse<List<string>>> GetFaculty(Priority priority, bool onlyFromCache = false)
        {
            return await BaseRequest(_simitApiService.GetApi(priority).GetFaculty(), onlyFromCache, nameof(Programma)).ConfigureAwait(false);
        }

    }
}
