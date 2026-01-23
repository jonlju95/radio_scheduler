using RadioScheduler.Extensions;

namespace RadioScheduler;

internal static class Program {
	private static void Main(string[] args) {
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.ConfigureConfiguration();
		builder.ConfigureAuth();
		builder.ConfigureLogging();
		builder.ConfigureServices();
		builder.ConfigureDatabase();
		builder.ConfigureCors();
		builder.ConfigureSwagger();

		WebApplicationBuilderExtensions.ConfigureDapper();

		WebApplication app = builder.Build();

		app.ConfigureMiddleware();
		app.ConfigureSwagger();
		app.UseAuthentication();
		app.UseAuthorization();

		app.Run();
	}
}
