using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class StudioService(IStudioRepository studioRepository) {
	public async Task<IEnumerable<Studio?>?> GetStudios(ApiResponse apiResponse) {
		IEnumerable<Studio?> studios = await studioRepository.GetStudios();

		if (studios != null) {
			return studios;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return studios;
	}

	public async Task<Studio?> GetStudio(ApiResponse apiResponse, Guid id) {
		Studio? studio = await studioRepository.GetStudio(id);

		if (studio != null) {
			return studio;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Studio not found" });
		apiResponse.Success = false;

		return studio;
	}

	public async Task<Studio?> CreateStudio(ApiResponse apiResponse, Studio studio) {
		if (studio == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Studio data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (this.GetStudio(apiResponse, studio.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Studio already exists" });
			apiResponse.Success = false;
			return null;
		}

		Studio newStudio = new Studio(studio);

		await studioRepository.CreateStudio(newStudio);
		return newStudio;
	}

	public async Task<bool> UpdateStudio(ApiResponse apiResponse, Guid id, Studio updatedStudio) {
		if (updatedStudio == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Studio data not provided" });
			apiResponse.Success = false;
			return false;
		}

		Studio? existingStudio = await this.GetStudio(apiResponse, id);
		if (existingStudio == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Studio not found" });
			apiResponse.Success = false;
			return false;
		}

		Studio newStudio = new Studio(updatedStudio);

		await studioRepository.UpdateStudio(newStudio);
		return true;
	}

	public async Task<bool> DeleteStudio(ApiResponse apiResponse, Guid id) {
		Studio? studioToDelete = await this.GetStudio(apiResponse, id);
		if (studioToDelete == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Studio not found" });
			apiResponse.Success = false;
			return false;
		}

		await studioRepository.DeleteStudio(id);
		return true;
	}
}
