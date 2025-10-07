using RadioScheduler.Utils.Middleware;

namespace RadioScheduler.Extensions;

internal static class WebApplicationExtensions {
	public static void ConfigureMiddleware(this WebApplication app) {
		app.UseCors("AllowAll");
		app.MapControllers();
		app.UsePathBase(new PathString("/v1"));
		app.UseRequestId();
	}

	public static void ConfigureSwagger(this WebApplication app) {
		if (!app.Environment.IsDevelopment()) {
			return;
		}
		app.MapOpenApi();
		app.UseSwagger();
		app.UseSwaggerUI();
	}
}
