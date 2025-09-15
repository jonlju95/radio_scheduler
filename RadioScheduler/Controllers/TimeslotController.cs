using System.Collections;
using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class TimeslotController(TimeslotService timeslotService, ApiResponse apiResponse)
	: BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetTimeslots() {
		IEnumerable<Timeslot> timeslots = await timeslotService.GetTimeslots();

		return timeslots == null || !timeslots.Any() ? this.NotFoundResponse() : this.SuccessResponse(timeslots);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetTimeslot(Guid id) {
		Timeslot? timeslot = await timeslotService.GetTimeslot(id);

		return timeslot == null ? this.NotFoundResponse() : this.SuccessResponse(timeslot);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateTimeslot([FromBody] Timeslot timeslot) {
		if (timeslot == null) {
			return this.BadRequestResponse();
		}

		Timeslot? newTimeslot = await timeslotService.CreateTimeslot(timeslot);

		return newTimeslot == null
			? this.ConflictResponse("Timeslot")
			: this.CreatedResponse(nameof(timeslot), timeslot, newTimeslot);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateTimeslot(Guid id, [FromBody] Timeslot timeslot) {
		if (timeslot == null) {
			return this.BadRequestResponse();
		}


		return await timeslotService.UpdateTimeslot(id, timeslot)
			? this.SuccessResponse(timeslot)
			: this.NoContentResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteTimeslot(Guid id) {
		return await timeslotService.DeleteTimeslot(id)
			? this.SuccessResponse()
			: this.NoContentResponse();
	}
}
