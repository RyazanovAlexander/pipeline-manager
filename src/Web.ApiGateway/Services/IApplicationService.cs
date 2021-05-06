using System;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    internal interface IApplicationService
    {
        Task<Guid> Create(Application application);
    }
}
