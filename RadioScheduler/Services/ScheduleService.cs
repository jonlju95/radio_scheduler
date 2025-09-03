using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class ScheduleService(IScheduleRepository scheduleRepository) {

	public IEnumerable<Schedule> GetSchedules() {
		return scheduleRepository.GetSchedules();
	}

	public Schedule? GetSchedule(Guid id) {
		return scheduleRepository.GetSchedule(id);
	}

	public Schedule CreateSchedule(Schedule schedule) {
		Schedule newSchedule = new Schedule(schedule);
		return scheduleRepository.CreateSchedule(newSchedule);
	}

	public bool UpdateSchedule(Guid id, Schedule updatedSchedule) {
		Schedule? existingSchedule = this.GetSchedule(id);
		if (existingSchedule == null) {
			return false;
		}

		Schedule newSchedule = new Schedule(updatedSchedule) {
			WeekNumber = updatedSchedule.WeekNumber,
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
