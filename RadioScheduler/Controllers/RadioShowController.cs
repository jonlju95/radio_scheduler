using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioShowController(RadioShowService radioShowService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<RadioShow>>> GetRadioShows() {
		IEnumerable<RadioShow> radioShows = radioShowService.GetRadioShows();

		return radioShows is null
			? this.FailResponse<List<RadioShow>>("NOT_FOUND", $"List {radioShows} not found")
			: this.OkResponse(radioShows.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<RadioShow>> GetRadioShow(Guid id) {
		RadioShow? radioShow = radioShowService.GetRadioShow(id);
		return radioShow is null
			? this.FailResponse<RadioShow>("NOT_FOUND", $"Radio show {radioShow} not found")
			: this.OkResponse(radioShow);
	}

	[HttpPost]
	public ActionResult<ResponseObject<RadioShow>> AddRadioShow([FromBody] RequestObject<RadioShow> request) {
		if (request.Data is null) {
			return this.FailResponse<RadioShow>("BAD_REQUEST", "No show provided");
		}

		RadioShow newRadioShow = radioShowService.AddRadioShow(request.Data);
		return this.OkResponse(newRadioShow);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateRadioShow(Guid id, [FromBody] RequestObject<RadioShow> request) {
		if (request.Data is null) {
			return this.FailResponse<string>("BAD_REQUEST", "No show provided");
		}

		bool success = radioShowService.UpdateRadioShow(id, request.Data);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "No show provided")
			: this.OkResponse("Radio show updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteRadioShow(Guid id) {
		bool success = radioShowService.DeleteRadioShow(id);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "No show found")
			: this.OkResponse("Radio show deleted");
	}
}
