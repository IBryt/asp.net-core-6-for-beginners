using Core.Services;
using Microsoft.Extensions.Options;

namespace Core;

public class CustomMiddleware2
{
    private readonly RequestDelegate _next;


    public CustomMiddleware2(RequestDelegate next)
    {
        _next = next;

    }

    public async Task Invoke(
        HttpContext context,
        IResponseFormatter formatter1,
        IResponseFormatter formatter2,
        IResponseFormatter formatter3,
        IResponseFormatter formatter4
        )
    {
        if (context.Request.Path == "/middleware2")
        {
            await formatter1.Format(context, string.Empty);
            await formatter2.Format(context, string.Empty);
            await formatter3.Format(context, string.Empty);
            await formatter4.Format(context, string.Empty);
        }
        else
        {
            await _next(context);
        }
    }
}
