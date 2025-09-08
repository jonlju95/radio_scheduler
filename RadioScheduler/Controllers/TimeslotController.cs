using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class TimeslotController(TimeslotService timeslotService, ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetTimeslots() {
		apiResponse.Data = await timeslotService.GetTimeslots(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetTimeslot(Guid id) {
		apiResponse.Data = await timeslotService.GetTimeslot(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> CreateTimeslot([FromBody] Timeslot timeslot) {
		apiResponse.Data = await timeslotService.CreateTimeslot(apiResponse, timeslot);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateTimeslot(Guid id, [FromBody] Timeslot timeslot) {
		apiResponse.Data = await timeslotService.UpdateTimeslot(apiResponse, id, timeslot);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteTimeslot(Guid id) {
		apiResponse.Data = await timeslotService.DeleteTimeslot(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
