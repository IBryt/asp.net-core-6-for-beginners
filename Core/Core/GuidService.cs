using Core.Services;

namespace Core;

public class GuidService : IResponseFormatter
{
    private readonly Guid guid = Guid.NewGuid();

    public async Task Format(HttpContext context, string content)
    {
        await context.Response.WriteAsync($"Guid {guid} \n<h2>{content}</h2>");
    }
}
