using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class ScheduleRepository : IScheduleRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);

	public async Task<IEnumerable<Schedule>> GetSchedules() {
		const string sql = "SELECT id, year, month FROM global_schedule";

		return await dbConnection.QueryAsync<Schedule>(sql);
	}

	public async Task<Schedule?> GetSchedule(Guid id) {
		const string sql = "SELECT id, year, month FROM global_schedule WHERE id = @id";

		return await dbConnection.QueryFirstOrDefaultAsync<Schedule>(sql, new { id = id.ToString("D").ToLower() });
	}

	public async Task<Schedule?> GetDailySchedule(DateOnly date) {
		const string sql =
			"SELECT id, start_date, end_date, week_no FROM global_schedule " +
			"WHERE @date BETWEEN date(start_date, 'unixepoch') AND date(end_date, 'unixepoch')";

		return await dbConnection.QueryFirstOrDefaultAsync<Schedule>(sql, new { date });
	}

	public async Task CreateSchedule(Schedule schedule) {
		const string sql =
			"INSERT INTO global_schedule (id, year, month) VALUES (@id, @year, @month)";

		await dbConnection.ExecuteAsync(sql,
			new { id = schedule.Id, year = schedule.Year, month = schedule.Month });
	}

	public async Task UpdateSchedule(Schedule newSchedule) {
		const string sql = "UPDATE global_schedule SET year = @year, month = @month WHERE id = @id";

		await dbConnection.ExecuteAsync(sql,
			new {
				newSchedule.Year, newSchedule.Month,
				id = newSchedule.Id.ToString("D").ToLower()
			});
	}

	public async Task DeleteSchedule(Guid id) {
		const string sql = "DELETE FROM global_schedule WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}
}
