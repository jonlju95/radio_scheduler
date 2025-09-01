using RadioScheduler.Interfaces;
using RadioScheduler.Repositories;
using RadioScheduler.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
builder.Services.AddScoped<RadioShowService>();

WebApplication app = builder.Build();

app.MapControllers();


app.Run();
