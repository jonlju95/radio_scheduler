using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IStudioRepository {
	IEnumerable<Studio> GetStudios();
	Studio? GetStudio(Guid id);
	Studio CreateStudio(Studio studio);
	void UpdateStudio(Studio existingStudio, Studio updatedStudio);
	void DeleteStudio(Studio studioToDelete);
}
