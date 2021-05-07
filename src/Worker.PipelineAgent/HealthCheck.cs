using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using Worker.PipelineAgent.Services;

namespace Worker.PipelineAgent
{
    public class HealthCheck : IHealthCheck
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheck(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_healthCheckService.IsReady())
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
