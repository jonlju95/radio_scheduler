using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioHostRepository : IRadioHostRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);


	public async Task<IEnumerable<RadioHost>> GetHosts() {
		const string sql = "SELECT id, first_name, last_name, is_guest FROM global_radio_host;";

		return await dbConnection.QueryAsync<RadioHost>(sql);
	}

	public async Task<RadioHost?> GetHost(Guid id) {
		const string sql = "SELECT id, first_name, last_name, is_guest FROM global_radio_host where id = @id";

		return await dbConnection.QuerySingleOrDefaultAsync<RadioHost>(sql, new { id = id.ToString("D").ToLower() });
	}

	public async Task CreateHost(RadioHost host) {
		const string sql =
			"INSERT INTO global_radio_host (id, first_name, last_name, is_guest) VALUES (@id, @firstName, @lastName, @isGuest)";

		await dbConnection.ExecuteAsync(sql,
			new {
				id = host.Id, firstName = host.FirstName, lastName = host.LastName, isGuest = host.IsGuest
			});
	}

	public async Task UpdateHost(RadioHost newHost) {
		const string sql =
			"UPDATE global_radio_host SET first_name = @firstName, last_name = @lastName, is_guest = @isGuest WHERE id = @id";

		await dbConnection.ExecuteAsync(sql,
			new {
				firstName = newHost.FirstName, lastName = newHost.LastName, isGuest = newHost.IsGuest,
				id = newHost.Id.ToString("D").ToLower()
			});
	}

	public async Task DeleteHost(Guid id) {
		const string sql = "DELETE FROM global_radio_host WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}
}
