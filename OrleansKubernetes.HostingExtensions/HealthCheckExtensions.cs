using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OrleansKubernetes.HostingExtensions;

public static class HealthCheckExtensions
{
    public static void AddCustomHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<HealthCheck>("OrleansKubernetes");
    }

    public static void MapCustomHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/startup");
        app.MapHealthChecks("/healthz");
        app.MapHealthChecks("/ready");
    }
}
