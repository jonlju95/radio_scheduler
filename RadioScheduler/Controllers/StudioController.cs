using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class StudioController(
	StudioService studioService,
	ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetStudios() {
		apiResponse.Data = await studioService.GetStudios(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetStudio(Guid id) {
		apiResponse.Data = await studioService.GetStudio(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> CreateStudio([FromBody] Studio studio) {
		apiResponse.Data = await studioService.CreateStudio(apiResponse, studio);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateStudio(Guid id, [FromBody] Studio studio) {
		apiResponse.Data = await studioService.UpdateStudio(apiResponse, id, studio);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteStudio(Guid id) {
		apiResponse.Data = await studioService.DeleteStudio(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
