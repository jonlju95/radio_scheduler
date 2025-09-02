using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioHostRepository {
	IEnumerable<RadioHost> GetHosts();
	RadioHost? GetHost(Guid id);
	RadioHost CreateHost(RadioHost host);
	void UpdateHost(RadioHost existingHost, RadioHost newHost);
	void DeleteHost(RadioHost hostToDelete);
}
