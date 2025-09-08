using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IStudioRepository {
	Task<IEnumerable<Studio>> GetStudios();
	Task<Studio?> GetStudio(Guid id);
	Task CreateStudio(Studio studio);
	Task UpdateStudio(Studio updatedStudio);
	Task DeleteStudio(Guid id);
}
