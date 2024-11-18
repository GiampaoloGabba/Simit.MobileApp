using System.Collections.Generic;
using System.Threading.Tasks;
using Fusillade;
using Simit.Core.Models;

namespace Simit.Core.Api
{
    public interface ISimitApiManager
    {
        Task<ApApiResponse<List<Comunicazioni>>> GetComunicazioni(Priority priority, bool onlyFromCache);
        Task<ApApiResponse<List<Call4Talk>>> GetMedia(Priority priority, bool onlyFromCache);
        Task<ApApiResponse<List<Programma>>> GetProgramma(Priority priority, bool onlyFromCache);
    }
}
