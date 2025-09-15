using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class ScheduleController(ScheduleService scheduleService, ApiResponse apiResponse)
	: BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetSchedules() {
		IEnumerable<Schedule> schedules = await scheduleService.GetSchedules();

		return schedules == null || !schedules.Any() ? this.NotFoundResponse() : this.SuccessResponse(schedules);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetSchedule(Guid id) {
		Schedule? schedule = await scheduleService.GetSchedule(id);

		return schedule == null ? this.NotFoundResponse() : this.SuccessResponse(schedule);
	}

	[HttpGet("daily")]
	public async Task<ActionResult<ApiResponse>> GetDailySchedule([FromQuery] string? date) {
		if (!DateOnly.TryParse(date, out DateOnly parsedDate)) {
			return this.BadRequestResponse();
		}

		Schedule? schedule = await scheduleService.GetDailySchedule(parsedDate);

		return schedule == null ? this.NotFoundResponse() : this.SuccessResponse(schedule);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateSchedule([FromBody] Schedule schedule) {
		if (schedule == null) {
			return this.BadRequestResponse();
		}

		Schedule? newSchedule = await scheduleService.CreateSchedule(schedule);

		return newSchedule == null
			? this.ConflictResponse("Schedule")
			: this.CreatedResponse(nameof(newSchedule), schedule, newSchedule);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateSchedule(Guid id, [FromBody] Schedule schedule) {
		if (schedule == null) {
			return this.BadRequestResponse();
		}

		return await scheduleService.UpdateSchedule(id, schedule)
			? this.SuccessResponse(schedule)
			: this.NoContentResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteSchedule(Guid id) {
		return await scheduleService.DeleteSchedule(id)
			? this.SuccessResponse()
			: this.NoContentResponse();
	}
}
