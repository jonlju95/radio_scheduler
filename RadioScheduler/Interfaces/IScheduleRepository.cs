using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IScheduleRepository {
	IEnumerable<Schedule> GetSchedules();
	Schedule? GetSchedule(Guid id);
	Schedule? GetDailySchedule(DateOnly date);
	Schedule CreateSchedule(Schedule schedule);
	void UpdateSchedule(Schedule existingSchedule, Schedule newSchedule);
	void DeleteSchedule(Schedule schedule);
}
