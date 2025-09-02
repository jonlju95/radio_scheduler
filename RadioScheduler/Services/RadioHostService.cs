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

	public bool UpdateRadioHost(Guid id, RadioHost radioHost) {
		RadioHost? existingRadioHost = radioHostRepository.GetRadioHost(id);
		if (existingRadioHost == null) {
			return false;
		}
		radioHostRepository.UpdateRadioHost(existingRadioHost, radioHost);
		return true;
	}

	public bool DeleteRadioHost(Guid id) {
		RadioHost? existingRadioHost = radioHostRepository.GetRadioHost(id);
		if (existingRadioHost == null) {
			return false;
		}
		radioHostRepository.DeleteRadioHost(existingRadioHost);
		return true;
	}
}
