using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioShowRepository {
	Task<IEnumerable<RadioShow>> GetRadioShows();
	Task<RadioShow?> GetRadioShow(Guid id);
	Task CreateRadioShow(RadioShow radioShow);
	Task UpdateRadioShow(RadioShow newRadioShow);
	Task DeleteRadioShow(Guid id);
}
