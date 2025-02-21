using OrleansKubernetes.HostingExtensions;
using OrleansKubernetes.Worker;

var builder = WebApplication.CreateBuilder(args);
builder.UseOrleansClient(client =>
{
    client.UseAdoNetClustering(ado =>
    {
        ado.ConnectionString = builder.Configuration.GetConnectionString("PgSql");
        ado.Invariant = "Npgsql";
    });
});
builder.Services.AddHostedService<WorkerService>();
builder.Services.AddCustomHealthCheck();
builder.AddSerilog();

var app = builder.Build();
app.MapCustomHealthChecks();
app.Run();
