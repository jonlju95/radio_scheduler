using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioHostRepository {
	IEnumerable<RadioHost> GetRadioHosts();
	RadioHost? GetRadioHost(Guid id);
	RadioHost CreateRadioHost(RadioHost radioHost);
	void UpdateRadioHost(RadioHost existingRadioHost, RadioHost newRadioHost);
	void DeleteRadioHost(RadioHost radioHostToDelete);
}
