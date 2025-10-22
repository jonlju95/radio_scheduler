using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class TableauRepository(AppDbContext dbContext, IDbConnection dbConnection) : ITableauRepository {

	public async Task<IEnumerable<Tableau>> GetTableaux() {
		return await dbContext.Tableau.OrderBy(t => t.Date).ToListAsync();
	}

	public async Task<Tableau?> GetTableau(Guid id) {
		return await dbContext.Tableau.FindAsync(id);
	}

	public async Task CreateTableau(Tableau tableau) {
		dbContext.Tableau.Add(tableau);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateTableau(Tableau newTableau) {
		await dbContext.Tableau
			.Where(t => t.Id.Equals(newTableau.Id))
			.ExecuteUpdateAsync(tableau => tableau
				.SetProperty(t => t.Date, newTableau.Date)
				.SetProperty(t => t.ScheduleId, newTableau.ScheduleId));
	}

	public async Task DeleteTableau(Guid id) {
		await dbContext.Tableau.Where(t => t.Id.Equals(id)).ExecuteDeleteAsync();
	}

	public async Task<Tableau?> GetDailyTableau(DateOnly date) {
		return await dbContext.Tableau.FirstOrDefaultAsync(t => t.Date.Equals(date));
	}

	public async Task<IEnumerable<Tableau>> GetWeeklyTableaux(DateOnly startDate, DateOnly endDate) {
		return await dbContext.Tableau.Where(t => t.Date >= startDate && t.Date <= endDate).ToListAsync();
	}
}
