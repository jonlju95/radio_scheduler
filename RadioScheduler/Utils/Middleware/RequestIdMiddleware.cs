namespace RadioScheduler.Utils.Middleware;

public class RequestIdMiddleware(RequestDelegate next) {
	private readonly RequestDelegate Next = next;

	public async Task Invoke(HttpContext context) {
		string requestId = context.Request.Headers["X-Request-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();

		context.Items["RequestId"] = requestId;

		context.Response.Headers["X-Request-Id"] = requestId;

		await this.Next(context);
	}
}
