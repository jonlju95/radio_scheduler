using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioHostRepository {
	IEnumerable<RadioHost> GetRadioHosts();
	RadioHost? GetRadioHost(Guid id);
	RadioHost CreateRadioHost(RadioHost radioHost);
	void UpdateRadioHost(RadioHost radioHost);
	void DeleteRadioHost(Guid id);
}
