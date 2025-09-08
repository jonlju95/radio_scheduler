using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class RadioShowService(IRadioShowRepository radioShowRepository) {

	public async Task<IEnumerable<RadioShow>?> GetRadioShows(ApiResponse apiResponse) {
		IEnumerable<RadioShow> radioShows = await radioShowRepository.GetRadioShows();

		if (radioShows != null) {
			return radioShows;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return radioShows;
	}

	public async Task<RadioShow?> GetRadioShow(ApiResponse apiResponse, Guid id) {
		RadioShow? radioShow = await radioShowRepository.GetRadioShow(id);

		if (radioShow != null) {
			return radioShow;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Radio show not found" });
		apiResponse.Success = false;

		return radioShow;
	}

	public async Task<RadioShow?> CreateRadioShow(ApiResponse apiResponse, RadioShow radioShow) {
		if (radioShow == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Radio show data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (await this.GetRadioShow(apiResponse, radioShow.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Radio show already exists" });
			apiResponse.Success = false;
			return null;
		}

		RadioShow newRadioShow = new RadioShow(radioShow);

		await radioShowRepository.CreateRadioShow(newRadioShow);
		return newRadioShow;
	}

	public async Task<bool> UpdateRadioShow(ApiResponse apiResponse, Guid id, RadioShow updatedRadioShow) {
		if (updatedRadioShow == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Radio show data not provided" });
			apiResponse.Success = false;
			return false;
		}

		if (await this.GetRadioShow(apiResponse, updatedRadioShow.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Radio show already exists" });
			apiResponse.Success = false;
			return false;
		}

		RadioShow? existingShow = await this.GetRadioShow(apiResponse, id);
		if (existingShow == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Radio show not found" });
			apiResponse.Success = false;
			return false;
		}

		RadioShow newRadioShow = new RadioShow(updatedRadioShow);

		await radioShowRepository.UpdateRadioShow(newRadioShow);
		return true;
	}

	public async Task<bool> DeleteRadioShow(ApiResponse apiResponse, Guid id) {
		RadioShow? existingShow = await this.GetRadioShow(apiResponse, id);
		if (existingShow == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Radio show not found" });
			apiResponse.Success = false;
			return false;
		}

		await radioShowRepository.DeleteRadioShow(id);
		return true;
	}

}
