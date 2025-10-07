using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioShowRepository(AppDbContext dbContext, IDbConnection dbConnection) : IRadioShowRepository {

	public async Task<IEnumerable<RadioShow>> GetRadioShows() {
		return await dbContext.RadioShows.ToListAsync();
	}

	public async Task<RadioShow?> GetRadioShow(Guid id) {
		return await dbContext.RadioShows.FindAsync(id);
	}

	public async Task CreateRadioShow(RadioShow radioShow) {
		dbContext.RadioShows.Add(radioShow);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateRadioShow(RadioShow newRadioShow) {
		await dbContext.RadioShows
			.Where(s => s.Id.Equals(newRadioShow.Id))
			.ExecuteUpdateAsync(radioShow => radioShow
				.SetProperty(r => r.Title, newRadioShow.Title)
				.SetProperty(r => r.DurationMin, newRadioShow.DurationMin));
	}

	public async Task DeleteRadioShow(Guid id) {
		await dbContext.RadioShows.Where(rs => rs.Id.Equals(id)).ExecuteDeleteAsync();
	}
}
