using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models.Api;
using RadioScheduler.Models.Auth;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class UserController(UserService userService, ApiResponse apiResponse) : BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetUsers() {
		IEnumerable<User> users = await userService.GetUsers();

		return users == null || !users.Any()
			? this.NotFoundResponse()
			: this.SuccessResponse(users);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetUser(Guid id) {
		User? user = await userService.GetUser(id);

		return user == null ? this.NotFoundResponse() : this.SuccessResponse(user);
	}

	[HttpGet("{username}")]
	public async Task<ActionResult<ApiResponse>> GetUserByUsername(string username) {
		User? user = await userService.GetUserByUsername(username);

		return user == null ? this.NotFoundResponse() : this.SuccessResponse(user);
	}

	[HttpPut("{id:guid}")]
	[Authorize]
	public async Task<ActionResult<ApiResponse>> UpdateUser(Guid id, [FromBody] User user) {
		if (user == null) {
			return this.BadRequestResponse();
		}

		return await userService.UpdateUser(id, user)
			? this.SuccessResponse(user)
			: this.NotFoundResponse();
	}
}
