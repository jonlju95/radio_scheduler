using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioHostRepository {
	Task<IEnumerable<RadioHost>> GetHosts();
	Task<RadioHost?> GetHost(Guid id);
	Task CreateHost(RadioHost host);
	Task UpdateHost(RadioHost newHost);
	Task DeleteHost(Guid id);
}
