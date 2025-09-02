using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioTimeslotController(RadioTimeslotService radioTimeslotService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<RadioTimeslot>>> GetRadioTimeslots() {
		IEnumerable<RadioTimeslot> radioTimeslots = radioTimeslotService.GetRadioTimeslots();

		return radioTimeslots is null
			? this.FailResponse<List<RadioTimeslot>>("NOT_FOUND", $"List {radioTimeslots} not found")
			: this.OkResponse(radioTimeslots.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<RadioTimeslot>> GetRadioTimeslot(Guid id) {
		RadioTimeslot? radioTimeslot = radioTimeslotService.GetRadioTimeslot(id);

		return radioTimeslot is null
			? this.FailResponse<RadioTimeslot>("NOT_FOUND", $"RadioTimeslot not found {id}")
			: this.OkResponse(radioTimeslot);
	}

	[HttpPost]
	public ActionResult<ResponseObject<RadioTimeslot>>
		AddRadioTimeslot([FromBody] RequestObject<RadioTimeslot> request) {
		if (request.Data == null) {
			return this.FailResponse<RadioTimeslot>("BAD_REQUEST", $"No item provided");
		}

		RadioTimeslot newRadioTimeslot = radioTimeslotService.AddRadioTimeslot(request.Data);
		return this.OkResponse(newRadioTimeslot);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateRadioTimeslot(Guid id,
		[FromBody] RequestObject<RadioTimeslot> request) {
		if (request.Data is null) {
			return this.FailResponse<string>("BAD_REQUEST", "No item provided");
		}

		bool success = radioTimeslotService.UpdateRadioTimeslot(id, request.Data);
		return !success
			? this.FailResponse<string>("NOT_FOUND", $"RadioTimeslot not found {id}")
			: this.OkResponse("RadioTimeslot updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteRadioTimeslot(Guid id) {
		bool success = radioTimeslotService.DeleteRadioTimeslot(id);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "RadioTimeslot not found")
			: this.OkResponse("RadioTimeslot deleted");
	}
}
