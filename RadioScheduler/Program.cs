using RadioScheduler.Interfaces;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
builder.Services.AddSingleton<IRadioHostRepository, RadioHostRepository>();
builder.Services.AddSingleton<ITimeslotRepository, TimeslotRepository>();
builder.Services.AddSingleton<IStudioRepository, StudioRepository>();

builder.Services.AddScoped<RadioShowService>();
builder.Services.AddScoped<RadioHostService>();
builder.Services.AddScoped<TimeslotService>();
builder.Services.AddScoped<StudioService>();

WebApplication app = builder.Build();

app.MapControllers();

app.UsePathBase(new PathString("/v1"));

app.UseRequestId();

app.Run();
