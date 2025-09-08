using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class ScheduleService(
	IScheduleRepository scheduleRepository,
	TableauService tableauService) {

	public async Task<IEnumerable<Schedule>?> GetSchedules(ApiResponse apiResponse) {
		IEnumerable<Schedule> schedules = await scheduleRepository.GetSchedules();

		if (schedules != null) {
			return schedules;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return schedules;
	}

	public async Task<Schedule?> GetSchedule(ApiResponse apiResponse, Guid id) {
		Schedule? schedule = await scheduleRepository.GetSchedule(id);

		if (schedule != null) {
			return schedule;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Schedule not found" });
		apiResponse.Success = false;

		return schedule;
	}

	public async Task<Schedule?> GetDailySchedule(ApiResponse apiResponse, string? date) {
		if (!DateOnly.TryParse(date, out DateOnly parsedDate)) {
			apiResponse.Error.Add(new ErrorInfo { Code = "INVALID_DATE_ONLY", Message = "Invalid date format" });
			apiResponse.Success = false;
			return null;
		}

		Schedule? dailySchedule = await scheduleRepository.GetDailySchedule(parsedDate);

		if (dailySchedule == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Schedule not found" });
			apiResponse.Success = false;
			return null;
		}

		Tableau? dailyTableau = await tableauService.GetDailyTableau(apiResponse, parsedDate);

		if (dailyTableau != null) {
			// dailySchedule.TableauIds = [dailyTableau];
		}

		return dailySchedule;
	}

	public async Task<Schedule?> CreateSchedule(ApiResponse apiResponse, Schedule schedule) {
		if (schedule == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Schedule data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (await this.GetSchedule(apiResponse, schedule.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Schedule already exists" });
			apiResponse.Success = false;
			return null;
		}

		Schedule newSchedule = new Schedule(schedule);

		await scheduleRepository.CreateSchedule(newSchedule);
		return newSchedule;

		// newSchedule.TableauIds = tableauService.CreateTableauForSchedule(newSchedule.Id, newSchedule.StartDate, newSchedule.EndDate);
	}

	public async Task<bool> UpdateSchedule(ApiResponse apiResponse, Guid id, Schedule updatedSchedule) {
		if (updatedSchedule == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Schedule data not provided" });
			apiResponse.Success = false;
			return false;
		}

		Schedule? existingSchedule = await this.GetSchedule(apiResponse, id);
		if (existingSchedule == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Schedule not found" });
			apiResponse.Success = false;
			return false;
		}

		Schedule newSchedule = new Schedule(updatedSchedule);

		await scheduleRepository.UpdateSchedule(newSchedule);
		return true;
	}

	public async Task<bool> DeleteSchedule(ApiResponse apiResponse, Guid id) {
		Schedule? scheduleToDelete = await this.GetSchedule(apiResponse, id);
		if (scheduleToDelete == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Schedule not found" });
			apiResponse.Success = false;
			return false;
		}

		await scheduleRepository.DeleteSchedule(id);
		return true;
	}
}
