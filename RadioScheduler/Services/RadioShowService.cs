using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class RadioShowService(IRadioShowRepository radioShowRepository) {

	public IEnumerable<RadioShow> GetRadioShows() {
		return radioShowRepository.GetRadioShows();
	}

	public RadioShow? GetRadioShow(Guid id) {
		return radioShowRepository.GetRadioShow(id);
	}

	public RadioShow AddRadioShow(RadioShow radioShow) {
		return radioShowRepository.CreateRadioShow(radioShow);
	}

	public void UpdateRadioShow(RadioShow radioShow) {
		radioShowRepository.UpdateRadioShow(radioShow);
	}

	public void DeleteRadioShow(Guid id) {
		radioShowRepository.DeleteRadioShow(id);
	}

}
