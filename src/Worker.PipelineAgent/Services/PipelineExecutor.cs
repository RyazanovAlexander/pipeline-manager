using Common.Models;
using Microsoft.Extensions.Logging;

namespace Worker.PipelineAgent.Services
{
    internal sealed class PipelineExecutor : IPipelineExecutor
    {
        private readonly IExecutorHubService _executorHubService;
        private readonly ILogger<PipelineExecutor> _logger;

        public PipelineExecutor(
            IExecutorHubService executorHubService,
            ILogger<PipelineExecutor> logger)
        {
            _executorHubService = executorHubService;
            _logger = logger;
        }

        public PipelineExecutionResult Execute(Pipeline pipeline)
        {
            foreach (var command in pipeline.Commands)
            {
                var result = _executorHubService.ExecuteCommand(command.ExecutorName, command.CommandLine);
                if (!result.result)
                {
                    var message = string.Format("Executor {0} failed to execute the command: {1}", command.ExecutorName, result.message);
                    _logger.LogError(message);

                    return new PipelineExecutionResult
                    {
                        Result = false,
                        Message = message
                    };
                }
            }

            return new PipelineExecutionResult { Result = true };
        }
    }
}
