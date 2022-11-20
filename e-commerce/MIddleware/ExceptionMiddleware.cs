using System.Net;
using System.Text.Json;
using e_commerce.Errors;

namespace e_commerce.MIddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var res = _env.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace)
                : new ApiException((int)HttpStatusCode.InternalServerError);
            var json = JsonSerializer.Serialize(res);

            await ctx.Response.WriteAsync(json);
        }
    }
}