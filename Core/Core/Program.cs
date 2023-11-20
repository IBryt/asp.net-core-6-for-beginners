using Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<Middleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
