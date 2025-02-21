using OrleansDashboard;
using OrleansKubernetes.HostingExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.UseOrleans(silo =>
{
    silo.UseAdoNetClustering(ado =>
    {
        ado.ConnectionString = builder.Configuration.GetConnectionString("PgSql");
        ado.Invariant = "Npgsql";
    });
    silo.UseKubernetesHosting();
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
