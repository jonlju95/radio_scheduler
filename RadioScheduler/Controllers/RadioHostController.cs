using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioHostController(
	RadioHostService radioHostService,
	ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetRadioHosts() {
		apiResponse.Data = await radioHostService.GetHosts(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetRadioHost(Guid id) {
		apiResponse.Data = await radioHostService.GetHost(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> AddRadioHost([FromBody] RadioHost radioHost) {
		apiResponse.Data = await radioHostService.CreateHost(apiResponse, radioHost);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateRadioHost(Guid id, [FromBody] RadioHost radioHost) {
		apiResponse.Data = await radioHostService.UpdateHost(apiResponse, id, radioHost);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteRadioHost(Guid id) {
		apiResponse.Data = await radioHostService.DeleteHost(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
