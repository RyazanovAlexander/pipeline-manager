using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Worker.PipelineAgent.Services;

namespace Worker.PipelineAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PipelineAgentOptions>(Configuration.GetSection(PipelineAgentOptions.SectionName));
            services.AddHealthChecks().AddCheck<HealthCheck>("health_check");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Worker.PipelineAgent", Version = "v1" });
            });

            services.AddApplicationServices();
            services.AddHostedService(x => x.GetService<IExecutorHubService>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worker.PipelineAgent v1"));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/ready");
                endpoints.MapHealthChecks("/healthz");
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ExecutorHubService>();
            services.AddSingleton<IHealthCheckService>(x => x.GetRequiredService<ExecutorHubService>());
            services.AddSingleton<IExecutorHubService>(x => x.GetRequiredService<ExecutorHubService>());

            services.AddSingleton<IPipelineExecutor, PipelineExecutor>();

            return services;
        }
    }
}
