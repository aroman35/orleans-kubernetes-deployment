using Orleans.Clustering.Kubernetes;
using OrleansKubernetes.HostingExtensions;
using OrleansKubernetes.Worker;

var builder = WebApplication.CreateBuilder(args);
builder.UseOrleansClient(client =>
{
    if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
    {
        client.UseKubeGatewayListProvider();
    }
    else
    {
        client.UseLocalhostClustering();
    }
});
builder.Services.AddHostedService<WorkerService>();
builder.Services.AddCustomHealthCheck();
builder.AddSerilog();

var app = builder.Build();
app.MapCustomHealthChecks();
app.Run();
