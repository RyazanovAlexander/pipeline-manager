namespace Worker.PipelineAgent.Services
{
    public interface IHealthCheckService
    {
        bool IsReady();

        bool IsHealthy();
    }
}
