using Microsoft.Extensions.Logging;
using OrleansKubernetes.Core;

namespace OrleansKubernetes.Grains;

public class MasterGrain(IGrainContext grainContext, ILogger<MasterGrain> logger) : IGrainBase, IMasterGrain
{
    public Task SimulateSomeWork()
    {
        logger.LogInformation("Simulating master work on grain: {GrainId}", this.GetPrimaryKey());
        return Task.CompletedTask;
    }

    public IGrainContext GrainContext { get; } = grainContext;
}
