using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Worker.PipelineAgent.Models;

namespace Worker.PipelineAgent.Services
{
    internal class ExecutorHubService : IExecutorHubService
    {
        private readonly ILogger<ExecutorHubService> _logger;
        private readonly PipelineAgentOptions _pipelineAgentOptions;
        private readonly Dictionary<string, Executor> _executorRegistry = new();

        private bool initialized = false;

        public ExecutorHubService(
            IOptions<PipelineAgentOptions> pipelineAgentOptions,
            ILogger<ExecutorHubService> logger)
        {
            _pipelineAgentOptions = pipelineAgentOptions.Value;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var executorOptions in _pipelineAgentOptions.Executors)
                {
                    _executorRegistry.Add(executorOptions.Name, new Executor(executorOptions.Name, executorOptions.Target));
                }

                initialized = true;

                _logger.LogDebug("ExecutorHub has been successfully initialized");
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public bool IsReady() => initialized;

        public bool IsHealthy() => initialized;

        public (bool result, string message) ExecuteCommand(string executorName, string command)
        {
            if (!_executorRegistry.TryGetValue(executorName, out Executor executor))
            {
                _logger.LogError("Executor {0} not found", executorName);
                return (false, "Executor not found");
            }

            var result = executor.ExecuteCommand(command);
            if (!result.Result)
            {
                _logger.LogError("Executor {0} failed to execute command {1}: {2}", executorName, command, result.ErrorMessage);
                return (false, result.ErrorMessage);
            }

            _logger.LogDebug("Executor {0} has successfully completed the command {1}", executorName, command);

            return (true, string.Empty);
        }

        public void Dispose()
        {
            foreach (var executorKeyValue in _executorRegistry)
            {
                executorKeyValue.Value.Dispose();
            }
        }
    }
}
