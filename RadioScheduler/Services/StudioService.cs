using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class StudioService(IStudioRepository studioRepository) {
	public IEnumerable<Studio> GetStudios() {
		return studioRepository.GetStudios();
	}

	public Studio? GetStudio(Guid id) {
		return studioRepository.GetStudio(id);
	}

	public Studio CreateStudio(Studio studio) {
		Studio newStudio = new Studio(studio);
		return studioRepository.CreateStudio(newStudio);
	}

	public bool UpdateStudio(Guid id, Studio updatedStudio) {
		Studio? existingStudio = this.GetStudio(id);
		if (existingStudio == null) {
			return false;
		}

		Studio newStudio = new Studio(existingStudio) {
			Name = updatedStudio.Name,
			BookingPrice = updatedStudio.BookingPrice,
			Capacity = updatedStudio.Capacity,
		};
		studioRepository.UpdateStudio(existingStudio, newStudio);
		return true;
	}

	public bool DeleteStudio(Guid id) {
		Studio? studioToDelete = this.GetStudio(id);
		if (studioToDelete == null) {
			return false;
		}

		studioRepository.DeleteStudio(studioToDelete);
		return true;
	}
}
