var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/https", async (context) =>
{
    await context.Response.WriteAsync($"Http request: {context.Request.IsHttps}");
});

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.Run();