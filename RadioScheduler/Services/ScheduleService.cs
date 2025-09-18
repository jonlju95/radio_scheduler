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
		DateOnly queryDate = date != DateOnly.MinValue
			? date
			: DateOnly.FromDateTime(DateTime.Now);

		Schedule? dailySchedule =
			await scheduleRepository.GetDailySchedule(queryDate);

		if (dailySchedule == null) {
			return null;
		}

		Tableau? dailyTableau = await tableauService.GetDailyTableau(queryDate);

		if (dailyTableau != null) {
			dailySchedule.TableauIds = [dailyTableau.Id];
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
