using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    internal interface IProcessorService
    {
        Task Register(Processor processor);
    }
}
