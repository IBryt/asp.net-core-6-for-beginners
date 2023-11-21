using Microsoft.Extensions.Options;

namespace Core;

public class FruitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly FruitOptions _options;

    public FruitMiddleware(RequestDelegate next, IOptions<FruitOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/fruit")
        {
            await context.Response.WriteAsync($"{_options.Name}, {_options.Color}");
        }
        else
        {
            await _next(context);
        }
    }
}