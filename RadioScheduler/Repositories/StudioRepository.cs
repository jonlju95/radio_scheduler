using Dapper;
using Microsoft.Data.Sqlite;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class StudioRepository : IStudioRepository {
	private const string connectionString = "Data Source=localDB.db";
	private static SqliteConnection dbConnection => new SqliteConnection(connectionString);

	public async Task<IEnumerable<Studio>> GetStudios() {
		const string sql = "SELECT id, name, capacity, booking_price FROM global_studio";

		return await dbConnection.QueryAsync<Studio>(sql);
	}

	public async Task<Studio?> GetStudio(Guid id) {
		const string sql = "SELECT id, name, capacity, booking_price FROM global_studio WHERE id = @id";

		return await dbConnection.QueryFirstOrDefaultAsync<Studio>(sql, new { id });
	}

	public async Task CreateStudio(Studio studio) {
		const string sql =
			"INSERT INTO global_studio (id, name, capacity, booking_price) VALUES (@id, @name, @capacity, @bookingPrice)";

		await dbConnection.ExecuteAsync(sql,
			new {
				id = studio.Id, name = studio.Name, capacity = studio.Capacity,
				bookingPrice = studio.BookingPrice
			});
	}

	public async Task UpdateStudio(Studio updatedStudio) {
		const string sql =
			"UPDATE global_studio SET name = @name, capacity = @capacity, bookingPrice = @bookingPrice WHERE id = @id";

		await dbConnection.ExecuteAsync(sql,
			new {
				name = updatedStudio.Name, capacity = updatedStudio.Capacity, bookingPrice = updatedStudio.BookingPrice,
				id = updatedStudio.Id.ToString("D").ToLower()
			});
	}

	public async Task DeleteStudio(Guid id) {
		const string sql = "DELETE FROM global_studio WHERE id = @id";

		await dbConnection.ExecuteAsync(sql, new { id = id.ToString("D").ToLower() });
	}
}
