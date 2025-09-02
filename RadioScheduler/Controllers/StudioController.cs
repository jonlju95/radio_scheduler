using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class StudioController(StudioService studioService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<Studio>>> GetStudios() {
		IEnumerable<Studio> studios = studioService.GetStudios();

		return studios is null
			? this.FailResponse<List<Studio>>("NOT_FOUND", $"List {studios} not found")
			: this.OkResponse(studios.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<Studio>> GetStudio(Guid id) {
		Studio? studio = studioService.GetStudio(id);

		return studio is null
			? this.FailResponse<Studio>("NOT_FOUND", "Studio not found")
			: this.OkResponse(studio);
	}

	[HttpPost]
	public ActionResult<ResponseObject<Studio>> CreateStudio([FromBody] RequestObject<Studio> request) {
		if (request.Data == null) {
			return this.FailResponse<Studio>("NOT_FOUND", "No studio data provided");
		}

		Studio newStudio = studioService.CreateStudio(request.Data);
		return this.OkResponse(newStudio);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateStudio(Guid id, [FromBody] RequestObject<Studio> request) {
		if (request.Data == null) {
			return this.FailResponse<string>("NOT_FOUND", "No studio data provided");
		}

		bool success = studioService.UpdateStudio(id, request.Data);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "Studio not found")
			: this.OkResponse("Studio updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteStudio(Guid id) {
		bool success = studioService.DeleteStudio(id);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "Studio not found")
			: this.OkResponse("Studio deleted");
	}
}
