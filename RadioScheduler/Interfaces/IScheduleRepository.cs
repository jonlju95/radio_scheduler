using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IScheduleRepository {
	IEnumerable<Schedule> GetSchedules();
	Schedule? GetSchedule(Guid id);
	Schedule CreateSchedule(Schedule schedule);
	void UpdateSchedule(Schedule existingSchedule, Schedule newSchedule);
	void DeleteSchedule(Schedule schedule);
}
