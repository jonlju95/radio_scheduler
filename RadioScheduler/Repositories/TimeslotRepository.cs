using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TimeslotRepository : ITimeslotRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);


	public async Task<IEnumerable<Timeslot>> GetTimeslots() {
		const string sql = "SELECT id, start_time, end_time, tableau_id, show_id, studio_id FROM timeslot";

		return await dbConnection.QueryAsync<Timeslot>(sql);
	}

	public async Task<Timeslot?> GetTimeslot(Guid id) {
		const string sql =
			"SELECT id, start_time, end_time, tableau_id, show_id, studio_id FROM timeslot WHERE id = @id";

		return await dbConnection.QueryFirstOrDefaultAsync<Timeslot>(sql, new { id });
	}

	public async Task CreateTimeslot(Timeslot timeslot) {
		const string sql =
			"INSERT INTO timeslot (id, start_time, end_time, tableau_id, show_id, studio_id VALUES (@id, @startTime, @endTime, @tableauId, @showId, @studioId)";

		await dbConnection.ExecuteAsync(sql,
			new {
				id = timeslot.Id, startTime = timeslot.StartTime, endTime = timeslot.EndTime,
				tableauId = timeslot.TableauId, showId = timeslot.RadioShow?.Id,
				studioId = timeslot.Studio?.Id
			});
	}

	public async Task UpdateTimeslot(Timeslot newTimeslot) {
		const string sql =
			"UPDATE tableau SET start_time = @startTime, end_time = @endTime, show_id = @showId, studio_id = @studioId WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new {
			startTime = newTimeslot.StartTime, endTime = newTimeslot.EndTime,
			showId = newTimeslot.RadioShow?.Id,
			studioId = newTimeslot.Studio?.Id,
			id = newTimeslot.Id.ToString("D").ToLower()
		});
	}

	public async Task DeleteTimeslot(Guid id) {
		const string sql = "DELETE FROM timeslot WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}

	public Task<IEnumerable<Timeslot>> GetTimeslotByTableauId(Guid id) {
		const string sql = "SELECT id, start_time, end_time, tableau_id, show_id, studio_id FROM timeslot WHERE id = @tableauId";

		return dbConnection.QueryAsync<Timeslot>(sql, new { tableauId = id });
	}
}
