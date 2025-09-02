using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils.JsonReaders;

namespace RadioScheduler.Repositories;

public class StudioRepository : IStudioRepository {
	private readonly List<Studio> studios = StudioJsonReader.GetInMemoryStudios();

	public IEnumerable<Studio> GetStudios() {
		return studios;
	}

	public Studio? GetStudio(Guid id) {
		return studios.FirstOrDefault(s => s.Id == id);
	}

	public Studio CreateStudio(Studio studio) {
		if (studios.Contains(studio)) {
			return studio;
		}

		studios.Add(studio);
		return studio;
	}

	public void UpdateStudio(Studio existingStudio, Studio updatedStudio) {
		int index = studios.IndexOf(existingStudio);
		studios[index] = updatedStudio;
	}

	public void DeleteStudio(Studio studioToDelete) {
		studios.Remove(studioToDelete);
	}
}
