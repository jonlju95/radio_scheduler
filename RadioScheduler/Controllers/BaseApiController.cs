using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
[Produces("application/json")]
public abstract class BaseApiController(ApiResponse response) : ControllerBase {

	protected ActionResult<ApiResponse> SuccessResponse(object? data = null) {
		response.Data = data;
		return this.Ok(response);
	}

	protected ActionResult<ApiResponse> CreatedResponse(string actionName, object routeValues, object? data = null) {
		response.Data = data;
		return this.CreatedAtAction(actionName, routeValues, response);
	}

	protected ActionResult<ApiResponse> NoContentResponse() {
		response.Data = null;
		return this.NoContent();
	}

	protected ActionResult<ApiResponse> BadRequestResponse() {
		response.Data = null;
		response.Error.Add(new ErrorInfo("400", "Bad Request"));
		response.Success = false;
		return this.BadRequest(response);
	}

	protected ActionResult<ApiResponse> UnauthorizedResponse() {
		response.Data = null;
		response.Error.Add(new ErrorInfo("401", "Unauthorized"));
		response.Success = false;
		return this.Unauthorized(response);
	}

	protected ActionResult<ApiResponse> ForbiddenResponse() {
		response.Data = null;
		response.Error.Add(new ErrorInfo("403", "Forbidden"));
		response.Success = false;
		return this.Forbid();
	}

	protected ActionResult<ApiResponse> NotFoundResponse() {
		response.Data = null;
		response.Error.Add(new ErrorInfo("404", "Not found"));
		response.Success = false;
		return this.NotFound(response);
	}

	protected ActionResult<ApiResponse> ConflictResponse(string typeName) {
		response.Data = null;
		response.Error.Add(new ErrorInfo("409", $"{typeName} already exists"));
		response.Success = false;
		return this.Conflict(response);
	}
}
