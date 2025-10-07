using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models.Api;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils;
using RadioScheduler.Utils.DatabaseHandlers;

namespace RadioScheduler.Extensions;

internal static class WebApplicationBuilderExtensions {
	public static void ConfigureLogging(this WebApplicationBuilder builder) {
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();
	}

	public static void ConfigureServices(this WebApplicationBuilder builder) {
		builder.Services.AddControllers();

		// Repositories
		builder.Services.AddScoped<IRadioShowRepository, RadioShowRepository>();
		builder.Services.AddScoped<IRadioHostRepository, RadioHostRepository>();
		builder.Services.AddScoped<IStudioRepository, StudioRepository>();
		builder.Services.AddScoped<ITimeslotRepository, TimeslotRepository>();
		builder.Services.AddScoped<ITableauRepository, TableauRepository>();
		builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

		// Services
		builder.Services.AddScoped<RadioShowService>();
		builder.Services.AddScoped<RadioHostService>();
		builder.Services.AddScoped<StudioService>();
		builder.Services.AddScoped<TimeslotService>();
		builder.Services.AddScoped<TableauService>();
		builder.Services.AddScoped<ScheduleService>();

		builder.Services.AddScoped<ApiResponse>();
	}

	public static void ConfigureDatabase(this WebApplicationBuilder builder) {
		builder.Services.AddDbContext<AppDbContext>(options => {
			options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services.AddScoped<IDbConnection>(provider => {
			AppDbContext context = provider.GetRequiredService<AppDbContext>();
			DbConnection connection = context.Database.GetDbConnection();
			if (connection.State != ConnectionState.Open) {
				connection.Open();
			}

			return connection;
		});
	}

	public static void ConfigureCors(this WebApplicationBuilder builder) {
		builder.Services.AddCors(options => {
			options.AddPolicy("AllowAll",
				policy => policy
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
		});
	}

	public static void ConfigureSwagger(this WebApplicationBuilder builder) {
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}

	public static void ConfigureDapper() {
		SqlMapper.AddTypeHandler(new GuidHandler());
		SqlMapper.AddTypeHandler(new UnixMsDateOnlyHandler());
		DefaultTypeMap.MatchNamesWithUnderscores = true;
	}
}
