using Orleans.Clustering.Kubernetes;
using OrleansDashboard;
using OrleansKubernetes.HostingExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.UseOrleans(silo =>
{
    if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
    {
        silo.UseKubernetesHosting();
        silo.UseKubeMembership();
    }
    else
    {
        silo.UseLocalhostClustering();
    }
    silo.ConfigureLogging(logging => logging.AddConsole());
    silo.UseDashboard(dashboard =>
    {
        var dashboardSettings = builder.Configuration.GetSection(nameof(DashboardOptions)).Get<DashboardOptions>();
        ArgumentNullException.ThrowIfNull(dashboardSettings);

        dashboard.Password = dashboardSettings.Password;
        dashboard.Username = dashboardSettings.Username;
        dashboard.HideTrace = dashboardSettings.HideTrace;
        dashboard.HostSelf = dashboardSettings.HostSelf;
        dashboard.Port = dashboardSettings.Port;
        dashboard.BasePath = dashboardSettings.BasePath;
        dashboard.CounterUpdateIntervalMs = dashboardSettings.CounterUpdateIntervalMs;
        dashboard.HistoryLength = dashboardSettings.HistoryLength;
    });
});
builder.Services.AddCustomHealthCheck();
builder.AddSerilog();

var app = builder.Build();
app.MapCustomHealthChecks();
app.Map("/dashboard", x => x.UseOrleansDashboard());
app.Run();
