using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class ScheduleRepository(AppDbContext dbContext, IDbConnection dbConnection) : IScheduleRepository {

	public async Task<IEnumerable<Schedule>> GetSchedules() {
		return await dbContext.Schedules.ToListAsync();
	}

	public async Task<Schedule?> GetSchedule(Guid id) {
		return await dbContext.Schedules.FindAsync(id);
	}

	public async Task<Schedule?> GetDailySchedule(DateOnly date) {
		return await dbContext.Schedules.FirstOrDefaultAsync(s => s.Month == date.Month && s.Year == date.Year);
	}

	public async Task CreateSchedule(Schedule schedule) {
		dbContext.Schedules.Add(schedule);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateSchedule(Schedule newSchedule) {
		await dbContext.Schedules
			.Where(s => s.Id.Equals(newSchedule.Id))
			.ExecuteUpdateAsync(schedule => schedule
				.SetProperty(s => s.Month, newSchedule.Month)
				.SetProperty(s => s.Year, newSchedule.Year));
	}

	public async Task DeleteSchedule(Guid id) {
		await dbContext.Schedules.Where(s => s.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
