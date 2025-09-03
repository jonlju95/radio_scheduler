using RadioScheduler.Interfaces;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils.Middleware;

namespace RadioScheduler;

internal static class Program {
	private static void Main(string[] args) {
		WebApplication app = SetupBuilderServices(args);

		app.MapControllers();
		app.UsePathBase(new PathString("/v1"));
		app.UseRequestId();

		app.Run();
	}

	private static WebApplication SetupBuilderServices(string[] args) {
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();

		// Add services to the container.
		builder.Services.AddControllers();

		builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
		builder.Services.AddSingleton<IRadioHostRepository, RadioHostRepository>();
		builder.Services.AddSingleton<IStudioRepository, StudioRepository>();
		builder.Services.AddSingleton<ITimeslotRepository, TimeslotRepository>();
		builder.Services.AddSingleton<ITableauRepository, TableauRepository>();

		builder.Services.AddScoped<RadioShowService>();
		builder.Services.AddScoped<RadioHostService>();
		builder.Services.AddScoped<StudioService>();
		builder.Services.AddScoped<TimeslotService>();
		builder.Services.AddScoped<TableauService>();

		return builder.Build();
	}
}
