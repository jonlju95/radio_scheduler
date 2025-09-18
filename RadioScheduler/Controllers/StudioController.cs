using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class StudioController(StudioService studioService, ApiResponse apiResponse) : BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetStudios() {
		IEnumerable<Studio> studios = await studioService.GetStudios();

		return studios == null || !studios.Any()
			? this.NotFoundResponse()
			: this.SuccessResponse(studios);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetStudio(Guid id) {
		Studio? studio = await studioService.GetStudio(id);

		return studio == null ? this.NotFoundResponse() : this.SuccessResponse(studio);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateStudio([FromBody] Studio studio) {
		if (studio == null) {
			return this.BadRequestResponse();
		}

		Studio? newStudio = await studioService.CreateStudio(studio);

		return newStudio == null
			? this.ConflictResponse("Studio")
			: this.CreatedResponse(nameof(this.GetStudio),new { id = newStudio.Id }, newStudio);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateStudio(Guid id, [FromBody] Studio studio) {
		if (studio == null) {
			return this.BadRequestResponse();
		}

		return await studioService.UpdateStudio(id, studio)
			? this.SuccessResponse(studio)
			: this.NotFoundResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteStudio(Guid id) {
		return await studioService.DeleteStudio(id)
			? this.NoContentResponse()
			: this.NotFoundResponse();
	}
}
