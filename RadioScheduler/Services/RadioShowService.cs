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
		if (radioShow.Id == Guid.Empty) {
			radioShow.Id = Guid.NewGuid();
		}
		return radioShowRepository.CreateRadioShow(radioShow);
	}

	public bool UpdateRadioShow(Guid id, RadioShow radioShow) {
		RadioShow? existingShow = radioShowRepository.GetRadioShow(id);
		if (existingShow == null) {
			return false;
		}
		radioShowRepository.UpdateRadioShow(existingShow, radioShow);
		return true;
	}

	public bool DeleteRadioShow(Guid id) {
		RadioShow? existingShow = radioShowRepository.GetRadioShow(id);
		if (existingShow == null) {
			return false;
		}

		radioShowRepository.DeleteRadioShow(existingShow);
		return true;
	}

}
