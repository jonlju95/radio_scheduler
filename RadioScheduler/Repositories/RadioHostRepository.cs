using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioHostRepository(AppDbContext dbContext, IDbConnection dbConnection) : IRadioHostRepository {

	public async Task<IEnumerable<RadioHost>> GetHosts() {
		return await dbContext.RadioHosts.ToListAsync();
	}

	public async Task<RadioHost?> GetHost(Guid id) {
		return await dbContext.RadioHosts.FindAsync(id);
	}

	public async Task CreateHost(RadioHost host) {
		dbContext.RadioHosts.Add(host);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateHost(RadioHost newHost) {
		await dbContext.RadioHosts
			.Where(rh => rh.Id.Equals(newHost.Id))
			.ExecuteUpdateAsync(radioHost => radioHost
				.SetProperty(r => r.FirstName, newHost.FirstName)
				.SetProperty(r => r.LastName, newHost.LastName)
				.SetProperty(r => r.IsGuest, newHost.IsGuest));
	}

	public async Task DeleteHost(Guid id) {
		await dbContext.RadioHosts.Where(rh => rh.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
