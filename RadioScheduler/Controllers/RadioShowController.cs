using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioShowController(
	RadioShowService radioShowService,
	ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetRadioShows() {
		apiResponse.Data = await radioShowService.GetRadioShows(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetRadioShow(Guid id) {
		apiResponse.Data = await radioShowService.GetRadioShow(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRadioShow([FromBody] RadioShow radioShow) {
		apiResponse.Data = await radioShowService.CreateRadioShow(apiResponse, radioShow);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateRadioShow(Guid id, [FromBody] RadioShow radioShow) {
		apiResponse.Data = await radioShowService.UpdateRadioShow(apiResponse, id, radioShow);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteRadioShow(Guid id) {
		apiResponse.Data = await radioShowService.DeleteRadioShow(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
