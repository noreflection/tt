using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

WebHost.CreateDefaultBuilder()
    // configure dependency injection and asp.net core services
    .ConfigureServices(services => { services.AddSingleton<ContactService>(); })
    // configure application pipeline
    .Configure(app =>
    {
        app.UseRouting();

        app.UseEndpoints(endpoint =>
        {
            ContactServiceEndpoint.MapEndpoint(endpoint);
            MapOtherEndpoints(endpoint);
        });
    })
    .Build()
    .Run();

static void MapOtherEndpoints(IEndpointRouteBuilder endpoint)
{
    endpoint.MapGet(
        "/",
        context => context.Response.WriteAsync("response from root url")
    );

    endpoint.MapGet(
        "/hello/{name}",
        context => context.Response.WriteAsync($"Hi, {context.Request.RouteValues["name"]}!")
    );
}

