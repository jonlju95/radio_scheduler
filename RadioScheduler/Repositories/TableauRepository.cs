using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TableauRepository : ITableauRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);

	public async Task<IEnumerable<Tableau>> GetTableaux() {
		const string sql = "SELECT id, date, schedule_id FROM Tableau";

		return await dbConnection.QueryAsync<Tableau>(sql);
	}

	public async Task<Tableau?> GetTableau(Guid id) {
		const string sql = "SELECT id, date, schedule_id FROM Tableau WHERE id = @id";

		return await dbConnection.QueryFirstOrDefaultAsync<Tableau>(sql, new { id });
	}

	public async Task CreateTableau(Tableau tableau) {
		const string sql = "INSERT INTO tableau (id, date, schedule_id) VALUES (@id, @date, @scheduleId)";

		await dbConnection.ExecuteAsync(sql,
			new { id = tableau.Id, date = tableau.Date, scheduleId = tableau.ScheduleId });
	}

	public async Task UpdateTableau(Tableau newTableau) {
		const string sql = "UPDATE tableau SET date = @date WHERE id = @id";

		await dbConnection.ExecuteAsync(sql,
			new { date = newTableau.Date, id = newTableau.Id.ToString("D").ToLower() });
	}

	public async Task DeleteTableau(Guid id) {
		const string sql = "DELETE FROM tableau WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}

	public async Task<Tableau?> GetDailyTableau(DateOnly date) {
		const string sql = "SELECT id, date, schedule_id FROM tableau WHERE date = @date";

		return await dbConnection.QueryFirstOrDefaultAsync<Tableau>(sql, new { date });
	}

	public async Task<IEnumerable<Tableau>> GetWeeklyTableaux(DateOnly startDate, DateOnly endDate) {
		const string sql = "SELECT id, date, schedule_id FROM tableau WHERE date BETWEEN @startDate AND @endDate";

		return await dbConnection.QueryAsync<Tableau>(sql, new { startDate, endDate });
	}
}
