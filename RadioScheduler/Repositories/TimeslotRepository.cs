using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class TimeslotRepository(AppDbContext dbContext, IDbConnection dbConnection) : ITimeslotRepository {

	public async Task<IEnumerable<Timeslot>> GetTimeslots() {
		return await dbContext.Timeslot.ToListAsync();
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
		dbContext.Timeslot.Add(timeslot);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateTimeslot(Timeslot newTimeslot) {
		await dbContext.Timeslot
			.Where(t => t.Id.Equals(newTimeslot.Id))
			.ExecuteUpdateAsync(timeslot => timeslot
				.SetProperty(t => t.StartTime, newTimeslot.StartTime)
				.SetProperty(t => t.EndTime, newTimeslot.EndTime)
				.SetProperty(t => t.RadioShow, newTimeslot.RadioShow)
				.SetProperty(t => t.Studio, newTimeslot.Studio));
	}

	public async Task DeleteTimeslot(Guid id) {
		await dbContext.Timeslot.Where(t => t.Id.Equals(id)).ExecuteDeleteAsync();
	}

	public async Task<IEnumerable<Timeslot>> GetTimeslotByTableauId(Guid id) {
		return await dbContext.Timeslot.Where(t => t.TableauId.Equals(id)).ToListAsync();
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
