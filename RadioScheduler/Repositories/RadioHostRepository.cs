using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioHostRepository(AppDbContext dbContext, IDbConnection dbConnection) : IRadioHostRepository {

	public async Task<IEnumerable<RadioHost>> GetHosts() {
		return await dbContext.RadioHost.ToListAsync();
	}

	public async Task<RadioHost?> GetHost(Guid id) {
		return await dbContext.RadioHost.FindAsync(id);
	}

	public async Task CreateHost(RadioHost host) {
		dbContext.RadioHost.Add(host);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateHost(RadioHost newHost) {
		await dbContext.RadioHost
			.Where(rh => rh.Id.Equals(newHost.Id))
			.ExecuteUpdateAsync(radioHost => radioHost
				.SetProperty(r => r.FirstName, newHost.FirstName)
				.SetProperty(r => r.LastName, newHost.LastName)
				.SetProperty(r => r.IsGuest, newHost.IsGuest));
	}

	public async Task DeleteHost(Guid id) {
		await dbContext.RadioHost.Where(rh => rh.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
