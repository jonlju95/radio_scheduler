using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class StudioService(IStudioRepository studioRepository) {
	public async Task<IEnumerable<Studio>> GetStudios() {
		return await studioRepository.GetStudios();
	}

	public async Task<Studio?> GetStudio(Guid id) {
		return await studioRepository.GetStudio(id);
	}

	public async Task<Studio?> CreateStudio(Studio studio) {
		if (await this.GetStudio(studio.Id) != null) {
			return null;
		}

		Studio newStudio = new Studio(studio);

		await studioRepository.CreateStudio(newStudio);
		return newStudio;
	}

	public async Task<bool> UpdateStudio(Guid id, Studio updatedStudio) {
		if (await this.GetStudio(id) == null) {
			return false;
		}

		Studio newStudio = new Studio(updatedStudio);

		await studioRepository.UpdateStudio(newStudio);
		return true;
	}

	public async Task<bool> DeleteStudio(Guid id) {
		if (await this.GetStudio(id) == null) {
			return false;
		}

		await studioRepository.DeleteStudio(id);
		return true;
	}
}
