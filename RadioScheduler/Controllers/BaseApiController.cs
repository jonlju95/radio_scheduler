using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
public abstract class BaseApiController : ControllerBase {
	private string CurrentRequestId =>
		this.HttpContext.Items["RequestId"]?.ToString() ?? Guid.NewGuid().ToString();

	protected ActionResult<ResponseObject<T>> OkResponse<T>(T data) =>
		this.Ok(new ResponseObject<T> {
			Success = true,
			RequestId = this.CurrentRequestId,
			Data = data
		});

	protected ActionResult<ResponseObject<T>> FailResponse<T>(string code, string message) =>
		this.Ok(new ResponseObject<T> {
			Success = false,
			RequestId = this.CurrentRequestId,
			Error = new ErrorInfo { Code = code, Message = message }
		});
}
