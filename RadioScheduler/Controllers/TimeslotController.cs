using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class TimeslotController(TimeslotService timeslotService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<Timeslot>>> GetTimeslots() {
		IEnumerable<Timeslot> timeslots = timeslotService.GetTimeslots();

		return timeslots is null
			? this.FailResponse<List<Timeslot>>("NOT_FOUND", $"List {timeslots} not found")
			: this.OkResponse(timeslots.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<Timeslot>> GetTimeslot(Guid id) {
		Timeslot? timeslot = timeslotService.GetTimeslot(id);

		return timeslot is null
			? this.FailResponse<Timeslot>("NOT_FOUND", $"Timeslot not found {id}")
			: this.OkResponse(timeslot);
	}

	[HttpPost]
	public ActionResult<ResponseObject<Timeslot>>
		AddTimeslot([FromBody] RequestObject<Timeslot> request) {
		if (request.Data == null) {
			return this.FailResponse<Timeslot>("BAD_REQUEST", "No item provided");
		}

		Timeslot newTimeslot = timeslotService.CreateTimeslot(request.Data);
		return this.OkResponse(newTimeslot);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateTimeslot(Guid id,
		[FromBody] RequestObject<Timeslot> request) {
		if (request.Data is null) {
			return this.FailResponse<string>("BAD_REQUEST", "No item provided");
		}

		bool success = timeslotService.UpdateTimeslot(id, request.Data);
		return !success
			? this.FailResponse<string>("NOT_FOUND", $"Timeslot not found {id}")
			: this.OkResponse("Timeslot updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteTimeslot(Guid id) {
		bool success = timeslotService.DeleteTimeslot(id);
		return !success
			? this.FailResponse<string>("NOT_FOUND", "Timeslot not found")
			: this.OkResponse("Timeslot deleted");
	}
}
