using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class RadioHostService(IRadioHostRepository radioHostRepository) {

	public IEnumerable<RadioHost> GetRadioHosts() {
		return radioHostRepository.GetRadioHosts();
	}

	public RadioHost? GetRadioHost(Guid id) {
		return radioHostRepository.GetRadioHost(id);
	}

	public RadioHost AddRadioHost(RadioHost radioHost) {
		if (radioHost.Id == Guid.Empty) {
			radioHost.Id = Guid.NewGuid();
		}
		return radioHostRepository.CreateRadioHost(radioHost);
	}

	public void UpdateRadioHost(RadioHost radioHost) {
		radioHostRepository.UpdateRadioHost(radioHost);
	}

	public void DeleteRadioHost(Guid id) {
		radioHostRepository.DeleteRadioHost(id);
	}
}
