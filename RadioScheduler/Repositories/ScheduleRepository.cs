using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class ScheduleRepository : IScheduleRepository {
	private const string connectionString = "Data Source=localDB.db";

	public IEnumerable<Schedule> GetSchedules() {
		const string sql = "SELECT id, start_date, end_date FROM global_schedule";

		using SqliteConnection dbConnection = new SqliteConnection(connectionString);
		List<Schedule> result = dbConnection.Query<Schedule>(sql).ToList();

		return result;
	}

	public Schedule? GetSchedule(Guid id) {
		const string sql = "SELECT id, start_date, end_date FROM global_schedule WHERE id = @id";

		using SqliteConnection dbConnection = new SqliteConnection(connectionString);
		return dbConnection.QueryFirstOrDefault<Schedule>(sql, new { id = id.ToString() });
	}

	public Schedule? GetDailySchedule(DateOnly date) {
		const string sql =
			"SELECT id, start_date, end_date, week_no FROM global_schedule " +
			"WHERE @date BETWEEN date(start_date, 'unixepoch') AND date(end_date, 'unixepoch')";

		using SqliteConnection dbConnection = new SqliteConnection(connectionString);
		return dbConnection.QueryFirstOrDefault<Schedule>(sql, new { date });
	}

	public Schedule? CreateSchedule(Schedule schedule) {
		const string sql =
			"INSERT INTO global_schedule (id, start_date, end_date) VALUES (@id, @start_date, @end_date)";
		using SqliteConnection dbConnection = new SqliteConnection(connectionString);

		return dbConnection.Execute(sql,
			new { id = schedule.Id, start_date = schedule.StartDate, end_date = schedule.EndDate }) > 0
			? schedule
			: null;
	}

	public void UpdateSchedule(Schedule newSchedule) {
		const string sql = "UPDATE global_schedule SET start_date = @startDate, end_date = @endDate WHERE id = @id";
		using SqliteConnection dbConnection = new SqliteConnection(connectionString);
		dbConnection.Execute(sql, new { startDate = newSchedule.StartDate, endDate = newSchedule.EndDate });
	}

	public void DeleteSchedule(Guid id) {
		const string sql = "DELETE FROM global_schedule WHERE id = @id";
		using SqliteConnection dbConnection = new SqliteConnection(connectionString);
		dbConnection.Execute(sql, new { id = id.ToString() });
	}
}
