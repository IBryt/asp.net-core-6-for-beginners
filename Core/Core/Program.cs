using Core;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IResponseFormatter, GuidService>();
var app = builder.Build();

app.MapGet("/formatter1", async (HttpContext context, IResponseFormatter formatter) => 
{
    await formatter.Format(context, "formatter 1");
});

app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "formatter 2");
});
app.MapGet("/", () => "Hello World!");

app.MapGet("/endpoint", CustomEndpoint.Endpoint);
app.UseMiddleware<CustomMiddleware>();
app.UseMiddleware<CustomMiddleware2>();
app.Run();
