using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
[Produces("application/json")]
public abstract class BaseApiController(ApiResponse response) : ControllerBase {
	protected new readonly ApiResponse Response = response;

	protected ActionResult<ApiResponse> SuccessResponse(object? data = null) {
		Response.Data = data;
		return this.Ok(Response);
	}

	protected ActionResult<ApiResponse> CreatedResponse(string actionName, object routeValues, object? data = null) {
		Response.Data = data;
		return this.CreatedAtAction(actionName, routeValues, Response);
	}

	protected ActionResult<ApiResponse> NoContentResponse() {
		Response.Data = null;
		return this.NoContent();
	}

	protected ActionResult<ApiResponse> BadRequestResponse() {
		Response.Data = null;
		Response.Error.Add(new ErrorInfo("400", "Bad Request"));
		Response.Success = false;
		return this.BadRequest(Response);
	}

	protected ActionResult<ApiResponse> UnauthorizedResponse() {
		Response.Data = null;
		Response.Error.Add(new ErrorInfo("401", "Unauthorized"));
		Response.Success = false;
		return this.Unauthorized(Response);
	}

	protected ActionResult<ApiResponse> ForbiddenResponse() {
		Response.Data = null;
		Response.Error.Add(new ErrorInfo("403", "Forbidden"));
		Response.Success = false;
		return this.Forbid();
	}

	protected ActionResult<ApiResponse> NotFoundResponse() {
		Response.Data = null;
		Response.Error.Add(new ErrorInfo("404", "Not found"));
		Response.Success = false;
		return this.NotFound(Response);
	}

	protected ActionResult<ApiResponse> ConflictResponse(string typeName) {
		Response.Data = null;
		Response.Error.Add(new ErrorInfo("409", $"{typeName} already exists"));
		Response.Success = false;
		return this.Conflict(Response);
	}
}
