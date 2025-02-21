using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OrleansKubernetes.HostingExtensions;

public class HealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}
