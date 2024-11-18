using Fusillade;

namespace Simit.Core.Api.Infrastructure
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);
    }
}
