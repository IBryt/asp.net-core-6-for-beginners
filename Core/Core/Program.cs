using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.Configure<MvcNewtonsoftJsonOptions>((options) =>
{
    options.SerializerSettings.NullValueHandling = 
        Newtonsoft.Json.NullValueHandling.Ignore;
});

var app = builder.Build();

app.MapControllers();
//app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

//app.MapGet("/", () => "Hello World!");

app.Run();

//migration commands
//dotnet ef migrations add Initial
//dotnet ef database update 