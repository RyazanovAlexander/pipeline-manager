using System;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    internal interface IPipelineService
    {
        Task<Guid> RunAsync(Guid applicationId, Pipeline pipeline);

        Task<PipelineStatus> GetStatusAsync(Guid pipelineId);
    }
}
