using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IScheduleRepository {
	Task<IEnumerable<Schedule>> GetSchedules();
	Task<Schedule?> GetSchedule(Guid id);
	Task<Schedule?> GetDailySchedule(DateOnly date);
	Task CreateSchedule(Schedule schedule);
	Task UpdateSchedule(Schedule newSchedule);
	Task DeleteSchedule(Guid id);
}
