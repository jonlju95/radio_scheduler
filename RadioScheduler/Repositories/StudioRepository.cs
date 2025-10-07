using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class StudioRepository(AppDbContext dbContext, IDbConnection dbConnection) : IStudioRepository {

	public async Task<IEnumerable<Studio>> GetStudios() {
		return await dbContext.Studios.ToListAsync();
	}

	public async Task<Studio?> GetStudio(Guid id) {
		return await dbContext.Studios.FindAsync(id);
	}

	public async Task CreateStudio(Studio studio) {
		dbContext.Studios.Add(studio);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateStudio(Studio updatedStudio) {
		await dbContext.Studios
			.Where(s => s.Id.Equals(updatedStudio.Id))
			.ExecuteUpdateAsync(studio => studio
				.SetProperty(s => s.Name, updatedStudio.Name)
				.SetProperty(s => s.BookingPrice, updatedStudio.BookingPrice)
				.SetProperty(s => s.Capacity, updatedStudio.Capacity));
	}

	public async Task DeleteStudio(Guid id) {
		await dbContext.Studios.Where(s => s.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
