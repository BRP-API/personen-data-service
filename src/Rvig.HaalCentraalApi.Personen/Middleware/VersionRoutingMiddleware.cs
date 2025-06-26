using Microsoft.AspNetCore.Http;

namespace Rvig.HaalCentraalApi.Personen.Middleware;

public class GbaApiVersionRoutingMiddleware
{
    private readonly RequestDelegate _next;

    public GbaApiVersionRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("accept-gezag-version"))
        {
            context.Request.Path = "/haalcentraal/api/brp/v2/personen";
        }
        await _next(context);
    }
}
