using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioShowController(
	RadioShowService radioShowService,
	ApiResponse apiResponse) : BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetRadioShows() {
		IEnumerable<RadioShow> radioShows = await radioShowService.GetRadioShows();

		return radioShows == null || !radioShows.Any()
			? this.NotFoundResponse()
			: this.SuccessResponse(radioShows);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetRadioShow(Guid id) {
		RadioShow? radioShow = await radioShowService.GetRadioShow(id);

		return radioShow == null ? this.NotFoundResponse() : this.SuccessResponse(radioShow);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateRadioShow([FromBody] RadioShow radioShow) {
		if (radioShow == null) {
			return this.BadRequestResponse();
		}

		RadioShow? newRadioShow = await radioShowService.CreateRadioShow(radioShow);

		return newRadioShow == null
			? this.ConflictResponse("Radio show")
			: this.CreatedResponse(nameof(this.GetRadioShow), new { id = newRadioShow.Id }, newRadioShow);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateRadioShow(Guid id, [FromBody] RadioShow radioShow) {
		if (radioShow == null) {
			return this.BadRequestResponse();
		}

		return await radioShowService.UpdateRadioShow(id, radioShow)
			? this.SuccessResponse(radioShow)
			: this.NoContentResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteRadioShow(Guid id) {
		return await radioShowService.DeleteRadioShow(id)
			? this.SuccessResponse()
			: this.NoContentResponse();
	}
}
