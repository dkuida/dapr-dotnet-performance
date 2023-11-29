

using Dapr.Client;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprPerf
{
    public class Program
    
    {
        
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<StateStore>();
            
            builder.Services.AddDaprClient(clientBuilder =>
            {
                                                            
            });
            
            var client = new DaprClientBuilder().Build();
            
            var app = builder.Build();

            app.MapPost("/store", async ([FromBody] SamplePayload request ,[FromServices]StateStore stateStore) =>
            {
                try
                {
                    var storeRequest = new StoreRequest<SamplePayload>(new []{new StoreItem<SamplePayload>(request.Id, request)});
                    await stateStore.StoreAsync(storeRequest);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Results.BadRequest(e.Message);
                }
                                    
                return Results.Ok();
            });

            app.Run();
        }
    }
}
