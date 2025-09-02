using RadioScheduler.Interfaces;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
builder.Services.AddSingleton<IRadioHostRepository, RadioHostRepository>();
builder.Services.AddSingleton<IRadioTimeslotRepository, RadioTimeslotRepository>();

builder.Services.AddScoped<RadioShowService>();
builder.Services.AddScoped<RadioHostService>();
builder.Services.AddScoped<RadioTimeslotService>();

WebApplication app = builder.Build();

app.MapControllers();

app.UsePathBase(new PathString("/v1"));

app.UseRequestId();

app.Run();
