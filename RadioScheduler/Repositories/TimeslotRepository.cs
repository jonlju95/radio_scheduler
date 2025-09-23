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
		Dictionary<Guid, Timeslot> timeslotDictionary = new Dictionary<Guid, Timeslot>();

		const string sql =
			"SELECT ts.*, h.*, s.*, st.* FROM timeslot ts " +
			"LEFT JOIN timeslot_host th ON th.timeslot_id = ts.id " +
			"LEFT JOIN global_radio_host h ON h.id = th.host_id " +
			"LEFT JOIN global_radio_show s ON s.id = ts.show_id " +
			"LEFT JOIN global_studio st ON st.id = ts.studio_id " +
			"WHERE ts.id = @id " +
			"ORDER BY h.is_guest";

		await dbConnection.QueryAsync<Timeslot, RadioHost, RadioShow, Studio, Timeslot>(sql,
			(timeslot, host, show, studio) => {
				if (!timeslotDictionary.TryGetValue(timeslot.Id, out Timeslot? timeslotEntry)) {
					timeslotEntry = timeslot;
					timeslotEntry.RadioHosts = [];
					timeslot.RadioShow = show;
					timeslot.Studio = studio;
					timeslotDictionary.Add(timeslot.Id, timeslotEntry);
				}

				if (host != null) {
					timeslotEntry.RadioHosts.Add(host);
				}

				return timeslotEntry;
			}, new { id },
			splitOn: "Id,Id,Id");

		return timeslotDictionary.Values.FirstOrDefault();
	}

	public async Task CreateTimeslot(Timeslot timeslot) {
		const string sql =
			"INSERT INTO timeslot (id, start_time, end_time, tableau_id, show_id, studio_id) VALUES (@id, @startTime, @endTime, @tableauId, @showId, @studioId)";

		await dbConnection.ExecuteAsync(sql,
			new {
				id = timeslot.Id, startTime = timeslot.StartTime, endTime = timeslot.EndTime,
				tableauId = timeslot.TableauId, showId = timeslot.RadioShow?.Id,
				studioId = timeslot.Studio?.Id
			});
	}

	public async Task UpdateTimeslot(Timeslot newTimeslot) {
		const string sql =
			"UPDATE timeslot SET start_time = @startTime, end_time = @endTime, show_id = @showId, studio_id = @studioId WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new {
			startTime = newTimeslot.StartTime, endTime = newTimeslot.EndTime,
			showId = newTimeslot.RadioShow?.Id,
			studioId = newTimeslot.Studio?.Id,
			id = newTimeslot.Id.ToString("D").ToUpper()
		});
	}

	public async Task DeleteTimeslot(Guid id) {
		const string sql = "DELETE FROM timeslot WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToUpper() });
	}

	public Task<IEnumerable<Timeslot>> GetTimeslotByTableauId(Guid id) {
		const string sql =
			"SELECT id, start_time, end_time, tableau_id, show_id, studio_id FROM timeslot WHERE tableau_id = @tableauId";

		return dbConnection.QueryAsync<Timeslot>(sql, new { tableauId = id.ToString("D").ToUpper() });
	}

	public async Task CreateHostTimeslotConnection(Guid timeslotId, Guid hostId) {
		const string sql = "INSERT INTO timeslot_host (timeslot_id, host_id) VALUES (@timeslotId, @hostId)";

		await dbConnection.ExecuteAsync(sql, new { timeslotId, hostId });
	}

	public Task DeleteHostTimeslotConnection(Guid timeslotId, Guid hostId) {
		const string sql = "DELETE FROM timeslot_host WHERE timeslot_id = @timeslotId AND host_id = @hostId";

		return dbConnection.ExecuteAsync(sql, new { timeslotId, hostId });
	}
}
