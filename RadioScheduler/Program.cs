using Dapper;
using RadioScheduler.Interfaces;
using RadioScheduler.Models.Api;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils.DatabaseHandlers;
using RadioScheduler.Utils.Middleware;

namespace RadioScheduler;

internal static class Program {
	private static void Main(string[] args) {
		WebApplication app = SetupBuilderServices(args);
		SetupDatabaseHandlers();

		app.UseCors("AllowAll");

		app.MapControllers();
		app.UsePathBase(new PathString("/v1"));
		app.UseRequestId();

		if (app.Environment.IsDevelopment()) {
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.Run();
	}

	private static WebApplication SetupBuilderServices(string[] args) {
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();

		// Controllers
		builder.Services.AddControllers();

		// Repositories
		builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
		builder.Services.AddSingleton<IRadioHostRepository, RadioHostRepository>();
		builder.Services.AddSingleton<IStudioRepository, StudioRepository>();
		builder.Services.AddSingleton<ITimeslotRepository, TimeslotRepository>();
		builder.Services.AddSingleton<ITableauRepository, TableauRepository>();
		builder.Services.AddSingleton<IScheduleRepository, ScheduleRepository>();

		// Services
		builder.Services.AddScoped<RadioShowService>();
		builder.Services.AddScoped<RadioHostService>();
		builder.Services.AddScoped<StudioService>();
		builder.Services.AddScoped<TimeslotService>();
		builder.Services.AddScoped<TableauService>();
		builder.Services.AddScoped<ScheduleService>();

		builder.Services.AddScoped<ApiResponse>();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddCors(options => {
			options.AddPolicy("AllowAll",
				policy => policy
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
		});

		return builder.Build();
	}

	private static void SetupDatabaseHandlers() {
		SqlMapper.AddTypeHandler(new GuidHandler());
		SqlMapper.AddTypeHandler(new UnixMsDateOnlyHandler());

		DefaultTypeMap.MatchNamesWithUnderscores = true;
	}
}
