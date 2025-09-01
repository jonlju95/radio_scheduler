using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioShowRepository {
	IEnumerable<RadioShow> GetRadioShows();
	RadioShow? GetRadioShow(Guid id);
	RadioShow CreateRadioShow(RadioShow radioShow);
	void UpdateRadioShow(RadioShow radioShow);
	void DeleteRadioShow(Guid id);
}
