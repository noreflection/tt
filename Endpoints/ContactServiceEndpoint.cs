using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

public static class ContactServiceEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoint)
    {
        var contactService = endpoint.ServiceProvider.GetRequiredService<ContactService>();

        // GET
        endpoint.MapGet(
            "/contacts",
            async context =>
                await context.Response.WriteAsJsonAsync(await contactService.GetAll())
        );

        endpoint.MapGet(
            "/contacts/{id:int}",
            async context => await context.Response.WriteAsJsonAsync(
                await contactService.Get(
                    int.Parse((string) context.Request.RouteValues["id"] ?? string.Empty))
                )
        );

        // POST
        endpoint.MapPost(
            "contacts",
            async context => { await contactService.Add(await context.Request.ReadFromJsonAsync<Contact>()); }
        );

        // DELETE
        endpoint.MapDelete(
            "contacts/{id:int}",
            async context =>
            {
                await contactService.Delete(
                    int.Parse((string) context.Request.RouteValues["id"] ?? string.Empty)
                );
            }
        );
    }
}