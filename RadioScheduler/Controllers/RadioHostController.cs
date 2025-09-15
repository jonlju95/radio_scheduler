using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioHostController(
	RadioHostService radioHostService,
	ApiResponse apiResponse) : BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetRadioHosts() {
		IEnumerable<RadioHost> radioHosts = await radioHostService.GetHosts();

		return radioHosts == null || !radioHosts.Any()
			? this.NotFoundResponse()
			: this.SuccessResponse(radioHosts);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetRadioHost(Guid id) {
		RadioHost? radioHost = await radioHostService.GetHost(id);

		return radioHost == null
			? this.NotFoundResponse()
			: this.SuccessResponse(radioHost);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateRadioHost([FromBody] RadioHost radioHost) {
		if (radioHost == null) {
			return this.BadRequestResponse();
		}

		RadioHost? newRadioHost = await radioHostService.CreateHost(radioHost);

		return newRadioHost == null
			? this.ConflictResponse("Radio host")
			: this.CreatedResponse(nameof(radioHost), radioHost, newRadioHost);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateRadioHost(Guid id, [FromBody] RadioHost radioHost) {
		if (radioHost == null) {
			return this.BadRequestResponse();
		}

		return await radioHostService.UpdateHost(id, radioHost)
			? this.SuccessResponse(radioHost)
			: this.NotFoundResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteRadioHost(Guid id) {
		return await radioHostService.DeleteHost(id)
			? this.NoContentResponse()
			: this.NotFoundResponse();
	}
}
