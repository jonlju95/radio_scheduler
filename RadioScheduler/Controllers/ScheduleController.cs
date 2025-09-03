using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class ScheduleController(ScheduleService scheduleService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<Schedule>>> GetSchedules() {
		IEnumerable<Schedule> schedules = scheduleService.GetSchedules();

		return schedules == null
			? this.FailResponse<List<Schedule>>("NOT_FOUND", $"List {schedules} not found")
			: this.OkResponse(schedules.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<Schedule>> GetSchedule(Guid id) {
		Schedule? schedule = scheduleService.GetSchedule(id);

		return schedule == null
			? this.FailResponse<Schedule>("NOT_FOUND", $"Schedule {id} not found")
			: this.OkResponse(schedule);
	}

	[HttpPost]
	public ActionResult<ResponseObject<Schedule>> CreateSchedule([FromBody] RequestObject<Schedule> request) {
		if (request.Data == null) {
			return this.FailResponse<Schedule>("BAD_REQUEST", "No schedule data provided");
		}

		Schedule newSchedule = scheduleService.CreateSchedule(request.Data);
		return this.OkResponse(newSchedule);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateSchedule(Guid id, [FromBody] RequestObject<Schedule> request) {
		if (request.Data == null) {
			return this.FailResponse<string>("BAD_REQUEST", "No schedule data provided");
		}

		bool success = scheduleService.UpdateSchedule(id, request.Data);
		return !success ?
			this.FailResponse<string>("NOT_FOUND", $"Schedule {id} not found") :
			this.OkResponse("Schedule updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteSchedule(Guid id) {
		bool success = scheduleService.DeleteSchedule(id);
		return !success ?
			this.FailResponse<string>("NOT_FOUND", $"Schedule {id} not found") :
			this.OkResponse("Schedule deleted");
	}
}
