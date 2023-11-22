using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});


builder.Services.AddHsts((options) =>
{
    options.MaxAge = TimeSpan.FromDays(1);
    options.IncludeSubDomains = true;
});

var app = builder.Build();

app.MapGet("/https", async (context) =>
{
    await context.Response.WriteAsync($"Http request: {context.Request.IsHttps}");
});

app.UseHttpsRedirection();

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.MapGet("/", () => "Hello World!");

app.Run();

//migration commands
//dotnet ef migrations add Initial
//dotnet ef database update 