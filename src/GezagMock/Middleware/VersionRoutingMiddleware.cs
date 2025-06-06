namespace GezagMock.Middleware
{
    public class VersionRoutingMiddleware
    {
        private readonly RequestDelegate _next;

        public VersionRoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("accept-gezag-version"))
            {
                context.Request.Path = "/api/v2/OpvragenBevoegdheidTotGezag";
            }
            await _next(context);
        }
    }
}
