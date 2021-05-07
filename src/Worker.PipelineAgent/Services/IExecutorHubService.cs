using Microsoft.Extensions.Hosting;
using System;

namespace Worker.PipelineAgent.Services
{
    internal interface IExecutorHubService: IHostedService, IHealthCheckService, IDisposable
    {
        (bool result, string message) ExecuteCommand(string executorName, string command);
    }
}
