using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class ScheduleRepository(AppDbContext dbContext, IDbConnection dbConnection) : IScheduleRepository {

	public async Task<IEnumerable<Schedule>> GetSchedules() {
		return await dbContext.Schedule.ToListAsync();
	}

	public async Task<Schedule?> GetSchedule(Guid id) {
		return await dbContext.Schedule.FindAsync(id);
	}

	public async Task<Schedule?> GetDailySchedule(DateOnly date) {
		return await dbContext.Schedule.FirstOrDefaultAsync(s => s.Month == date.Month && s.Year == date.Year);
	}

	public async Task CreateSchedule(Schedule schedule) {
		dbContext.Schedule.Add(schedule);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateSchedule(Schedule newSchedule) {
		await dbContext.Schedule
			.Where(s => s.Id.Equals(newSchedule.Id))
			.ExecuteUpdateAsync(schedule => schedule
				.SetProperty(s => s.Month, newSchedule.Month)
				.SetProperty(s => s.Year, newSchedule.Year));
	}

	public async Task DeleteSchedule(Guid id) {
		await dbContext.Schedule.Where(s => s.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
