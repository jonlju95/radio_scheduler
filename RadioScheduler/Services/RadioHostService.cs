using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class RadioHostService(IRadioHostRepository radioHostRepository) {

	public IEnumerable<RadioHost> GetHosts() {
		return radioHostRepository.GetHosts();
	}

	public RadioHost? GetHost(Guid id) {
		return radioHostRepository.GetHost(id);
	}

	public RadioHost CreateHost(RadioHost host) {
		if (host.Id == Guid.Empty) {
			host.Id = Guid.NewGuid();
		}

		return radioHostRepository.CreateHost(host);
	}

	public bool UpdateHost(Guid id, RadioHost host) {
		RadioHost? existingHost = radioHostRepository.GetHost(id);
		if (existingHost == null) {
			return false;
		}

		radioHostRepository.UpdateHost(existingHost, host);
		return true;
	}

	public bool DeleteHost(Guid id) {
		RadioHost? existingHost = radioHostRepository.GetHost(id);
		if (existingHost == null) {
			return false;
		}

		radioHostRepository.DeleteHost(existingHost);
		return true;
	}
}
