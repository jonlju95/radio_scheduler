using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Repositories;
using RadioScheduler.Services;
using RadioScheduler.Utils.JsonReaders;
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

		List<Schedule> schedules = ScheduleJsonReader.GetInMemorySchedules();
		List<Tableau> tableaux = [];

		schedules.ForEach(schedule => tableaux.AddRange(schedule.Tableaux));

		List<Timeslot> timeslots = tableaux.SelectMany(tableau => tableau.Timeslots).ToList();


		// Controllers
		builder.Services.AddControllers();

		// Repositories
		builder.Services.AddSingleton<IRadioShowRepository, RadioShowRepository>();
		builder.Services.AddSingleton<IRadioHostRepository, RadioHostRepository>();
		builder.Services.AddSingleton<IStudioRepository, StudioRepository>();
		builder.Services.AddSingleton<ITimeslotRepository>(new TimeslotRepository(timeslots));
		builder.Services.AddSingleton<ITableauRepository>(new TableauRepository(tableaux));
		builder.Services.AddSingleton<IScheduleRepository>(new ScheduleRepository(schedules));

		// Services
		builder.Services.AddScoped<RadioShowService>();
		builder.Services.AddScoped<RadioHostService>();
		builder.Services.AddScoped<StudioService>();
		builder.Services.AddScoped<TimeslotService>();
		builder.Services.AddScoped<TableauService>();
		builder.Services.AddScoped<ScheduleService>();

		return builder.Build();
	}
}
