using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class RadioShowService(IRadioShowRepository radioShowRepository) {

	public async Task<IEnumerable<RadioShow>> GetRadioShows() {
		return await radioShowRepository.GetRadioShows();
	}

	public async Task<RadioShow?> GetRadioShow(Guid id) {
		return await radioShowRepository.GetRadioShow(id);
	}

	public async Task<RadioShow?> CreateRadioShow(RadioShow radioShow) {
		if (await this.GetRadioShow(radioShow.Id) != null) {
			return null;
		}

		RadioShow newRadioShow = new RadioShow(radioShow);

		await radioShowRepository.CreateRadioShow(newRadioShow);
		return newRadioShow;
	}

	public async Task<bool> UpdateRadioShow(Guid id, RadioShow updatedRadioShow) {
		if (await this.GetRadioShow(id) != null) {
			return false;
		}

		RadioShow newRadioShow = new RadioShow(updatedRadioShow);

		await radioShowRepository.UpdateRadioShow(newRadioShow);
		return true;
	}

	public async Task<bool> DeleteRadioShow(Guid id) {
		if (await this.GetRadioShow(id) == null) {
			return false;
		}

		await radioShowRepository.DeleteRadioShow(id);
		return true;
	}

}
