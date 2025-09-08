using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class ScheduleController(ScheduleService scheduleService, ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetSchedules() {
		apiResponse.Data = await scheduleService.GetSchedules(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetSchedule(Guid id) {
		apiResponse.Data = await scheduleService.GetSchedule(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpGet("daily")]
	public async Task<IActionResult> GetDailySchedule([FromQuery] string? date) {
		apiResponse.Data = await scheduleService.GetDailySchedule(apiResponse, date);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> CreateSchedule([FromBody] Schedule schedule) {
		apiResponse.Data = await scheduleService.CreateSchedule(apiResponse, schedule);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] Schedule schedule) {
		apiResponse.Data = await scheduleService.UpdateSchedule(apiResponse, id, schedule);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteSchedule(Guid id) {
		apiResponse.Data = await scheduleService.DeleteSchedule(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
