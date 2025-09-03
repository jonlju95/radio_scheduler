namespace RadioScheduler.Utils.Middleware;

public class RequestIdMiddleware(RequestDelegate next) {

	public async Task Invoke(HttpContext context) {
		string requestId = context.Request.Headers["X-Request-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

		context.Items["RequestId"] = requestId;

		context.Response.Headers["X-Request-Id"] = requestId;

		await next(context);
	}
}
