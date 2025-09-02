using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioShowRepository {
	IEnumerable<RadioShow> GetRadioShows();
	RadioShow? GetRadioShow(Guid id);
	RadioShow CreateRadioShow(RadioShow radioShow);
	void UpdateRadioShow(RadioShow existingRadioShow, RadioShow newRadioShow);
	void DeleteRadioShow(RadioShow radioShowToDelete);
}
