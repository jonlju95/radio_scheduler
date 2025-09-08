using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class RadioShowRepository : IRadioShowRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);

	public async Task<IEnumerable<RadioShow>> GetRadioShows() {
		const string sql = "SELECT id, name, duration_min FROM global_radio_show";

		return await dbConnection.QueryAsync<RadioShow>(sql);
	}

	public async Task<RadioShow?> GetRadioShow(Guid id) {
		const string sql = "SELECT id, name, duration_min FROM global_radio_show WHERE id = @id";

		return await dbConnection.QueryFirstOrDefaultAsync<RadioShow>(sql, new { id = id.ToString("D").ToLower() });
	}

	public async Task CreateRadioShow(RadioShow radioShow) {
		const string sql = "INSERT INTO global_radio_show (id, name, duration_min) " +
		                   "VALUES (@id, @name, @durationMin)";

		await dbConnection.ExecuteAsync(sql,
			new { id = radioShow.Id, name = radioShow.Name, durationMin = radioShow.DurationMinutes });
	}

	public async Task UpdateRadioShow(RadioShow newRadioShow) {
		const string sql = "UPDATE global_radio_show SET name = @name, duration_min = @durationMin WHERE id = @id";

		await dbConnection.ExecuteAsync(sql,
			new {
				name = newRadioShow.Name, durationMin = newRadioShow.DurationMinutes,
				id = newRadioShow.Id.ToString("D").ToLower()
			});
	}

	public async Task DeleteRadioShow(Guid id) {
		const string sql = "DELETE FROM global_radio_show WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}
}
