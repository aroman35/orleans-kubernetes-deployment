using OrleansKubernetes.Core;

namespace OrleansKubernetes.Worker;

public class WorkerService(IClusterClient clusterClient, ILogger<WorkerService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var masterGrain = clusterClient.GetGrain<IMasterGrain>(Guid.NewGuid());
                await masterGrain.SimulateSomeWork();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "An unexpected error occurred");
            }
        }
    }
}
