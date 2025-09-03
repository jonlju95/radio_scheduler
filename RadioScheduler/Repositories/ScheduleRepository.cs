using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class ScheduleRepository : IScheduleRepository {
	private readonly List<Schedule> schedules = [];

	public IEnumerable<Schedule> GetSchedules() {
		return schedules;
	}

	public Schedule? GetSchedule(Guid id) {
		return schedules.FirstOrDefault(s => s.Id == id);
	}

	public Schedule CreateSchedule(Schedule schedule) {
		if (schedules.Contains(schedule)) {
			return schedule;
		}
		schedules.Add(schedule);
		return schedule;
	}

	public void UpdateSchedule(Schedule existingSchedule, Schedule newSchedule) {
		int index = schedules.IndexOf(existingSchedule);
		schedules[index] = newSchedule;
	}

	public void DeleteSchedule(Schedule schedule) {
		schedules.Remove(schedule);
	}
}
