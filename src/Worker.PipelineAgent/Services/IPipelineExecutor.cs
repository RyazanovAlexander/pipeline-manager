using Common.Models;

namespace Worker.PipelineAgent.Services
{
    public interface IPipelineExecutor
    {
        PipelineExecutionResult Execute(Pipeline pipeline);
    }
}
