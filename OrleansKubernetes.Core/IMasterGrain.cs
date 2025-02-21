namespace OrleansKubernetes.Core;

public interface IMasterGrain : IGrainWithGuidKey
{
    Task SimulateSomeWork();
}
