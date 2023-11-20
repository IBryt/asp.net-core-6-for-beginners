using Core.Services;
using Microsoft.Extensions.Options;

namespace Core;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IResponseFormatter _formatter;

    public CustomMiddleware(RequestDelegate next, IResponseFormatter formatter)
    {
        _next = next;
        _formatter = formatter;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/middleware")
        {
            await _formatter.Format(context, "Custom Middleware 1");
        }
        else
        {
            await _next(context);
        }
    }
}
