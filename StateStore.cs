using System.Dynamic;
using Dapr.Client;

namespace DaprPerf;

public class SamplePayload : DynamicObject
{
    public string Id { get; set; }
}
public class StoreItem<T> {
   
    public StoreItem(string key, T value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public T Value { get; set; }
}

public class StoreRequest<T> {
    public StoreRequest(StoreItem<T>[] items)
    {
        Items = items;
    }

    public StoreItem<T>[] Items { get; set; }
}

public class StateStore
{
    private readonly DaprClient m_DaprClient;
    private readonly string m_StateStoreName = "statestore";


    public StateStore(DaprClient daprClient)
    {
        m_DaprClient = daprClient;
    }
    
     public Task StoreAsync<T>(StoreRequest<T> request)
    {
        var saveStateItems = request.Items.Select(i=> new SaveStateItem<T>(i.Key, i.Value, null, null, null)).ToList();

        return m_DaprClient.SaveBulkStateAsync<T>(m_StateStoreName,saveStateItems );
    }
    
}
