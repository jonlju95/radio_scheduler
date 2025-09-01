using RadioScheduler.Interfaces;
using RadioScheduler.Repositories;
using RadioScheduler.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

app.Run();
