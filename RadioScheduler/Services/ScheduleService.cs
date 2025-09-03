using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class ScheduleService(
	IScheduleRepository scheduleRepository,
	TableauService tableauService,
	TimeslotService timeslotService) {

	public IEnumerable<Schedule> GetSchedules() {
		return scheduleRepository.GetSchedules();
	}

	public Schedule? GetSchedule(Guid id) {
		return scheduleRepository.GetSchedule(id);
	}

	public Schedule? GetDailySchedule(DateOnly date) {
		Schedule? dailySchedule = scheduleRepository.GetDailySchedule(date);
		if (dailySchedule == null) {
			return null;
		}

		Tableau? dailyTableau = tableauService.GetDailyTableau(date);

		if (dailyTableau != null) {
			dailySchedule.Tableaux = [dailyTableau];
		}

		return dailySchedule;
	}

	public Schedule CreateSchedule(Schedule schedule) {
		Schedule newSchedule = new Schedule(
			Guid.NewGuid(),
			schedule.StartDate,
			schedule.EndDate == DateOnly.Parse("0001-01-01")
				? schedule.StartDate.AddDays(6)
				: schedule.EndDate
		);

		newSchedule.Tableaux = tableauService.CreateTableauForSchedule(newSchedule.StartDate, newSchedule.EndDate);
		return scheduleRepository.CreateSchedule(newSchedule);
	}

	public bool UpdateSchedule(Guid id, Schedule updatedSchedule) {
		Schedule? existingSchedule = this.GetSchedule(id);
		if (existingSchedule == null) {
			return false;
		}

		Schedule newSchedule = new Schedule(updatedSchedule) {
			StartDate = updatedSchedule.StartDate,
			EndDate = updatedSchedule.EndDate,
			Tableaux = updatedSchedule.Tableaux
		};

		scheduleRepository.UpdateSchedule(existingSchedule, newSchedule);
		return true;
	}

	public bool DeleteSchedule(Guid id) {
		Schedule? scheduleToDelete = this.GetSchedule(id);
		if (scheduleToDelete == null) {
			return false;
		}

		scheduleRepository.DeleteSchedule(scheduleToDelete);
		return true;
	}
}
