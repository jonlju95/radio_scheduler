using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class ScheduleService(
	IScheduleRepository scheduleRepository,
	TableauService tableauService) {

	public async Task<IEnumerable<Schedule>> GetSchedules() {
		return await scheduleRepository.GetSchedules();
	}

	public async Task<Schedule?> GetSchedule(Guid id) {
		return await scheduleRepository.GetSchedule(id);
	}

	public async Task<Schedule?> GetDailySchedule(DateOnly date) {
		Schedule? dailySchedule = await scheduleRepository.GetDailySchedule(date);

		if (dailySchedule == null) {
			// apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Schedule not found" });
			// apiResponse.Success = false;
			return null;
		}

		Tableau? dailyTableau = await tableauService.GetDailyTableau(date);

		if (dailyTableau != null) {
			// dailySchedule.TableauIds = [dailyTableau];
		}

		return dailySchedule;
	}

	public async Task<Schedule?> CreateSchedule(Schedule schedule) {
		if (await this.GetSchedule(schedule.Id) != null) {
			return null;
		}

		Schedule newSchedule = new Schedule(schedule.Id, schedule.Year, schedule.Month);

		await scheduleRepository.CreateSchedule(newSchedule);

		newSchedule.TableauIds =
			await tableauService.CreateTableauForSchedule(newSchedule.Id, newSchedule.Year, newSchedule.Month);

		return newSchedule;
	}

	public async Task<bool> UpdateSchedule(Guid id, Schedule updatedSchedule) {
		if (await this.GetSchedule(id) == null) {
			return false;
		}

		Schedule newSchedule = new Schedule(updatedSchedule.Id, updatedSchedule.Year, updatedSchedule.Month);

		await scheduleRepository.UpdateSchedule(newSchedule);
		return true;
	}

	public async Task<bool> DeleteSchedule(Guid id) {
		if (await this.GetSchedule(id) == null) {
			return false;
		}

		await scheduleRepository.DeleteSchedule(id);
		return true;
	}
}
