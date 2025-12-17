using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;
using RadioScheduler.Models.Auth;
using RadioScheduler.Services.Auth;

namespace RadioScheduler.Controllers.Auth;

[Route("auth")]
public class AuthController(AuthService authService, TokenService tokenService, ApiResponse apiResponse)
	: BaseApiController(apiResponse) {

	[HttpPost("login")]
	public async Task<ActionResult<ApiResponse>> LoginUser([FromBody] LoginRequest request) {
		if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)) {
			return this.BadRequestResponse();
		}

		User? user = await authService.LoginUser(request.Username, request.Password);
		if (user == null) {
			return this.UnauthorizedResponse();
		}

		string token = tokenService.GenerateToken(user);

		var loginResult = new {
			token,
			user = new User(user)
		};

		return this.SuccessResponse(loginResult);
	}
}
