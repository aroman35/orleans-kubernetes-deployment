using Microsoft.AspNetCore.Builder;
using Serilog;

namespace OrleansKubernetes.HostingExtensions;

public static class HostBuilderExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostContext, logger) => logger.ReadFrom.Configuration(hostContext.Configuration));
    }
}
