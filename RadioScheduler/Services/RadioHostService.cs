using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class RadioHostService(IRadioHostRepository radioHostRepository) {

	public async Task<IEnumerable<RadioHost>> GetHosts() {
		return await radioHostRepository.GetHosts();
	}

	public async Task<RadioHost?> GetHost(Guid id) {
		return await radioHostRepository.GetHost(id);
	}

	public async Task<RadioHost?> CreateHost(RadioHost host) {
		if (await this.GetHost(host.Id) != null) {
			return null;
		}

		RadioHost newRadioHost = new RadioHost(host);

		await radioHostRepository.CreateHost(newRadioHost);
		return newRadioHost;
	}

	public async Task<bool> UpdateHost(Guid id, RadioHost updatedHost) {
		if (await this.GetHost(id) == null) {
			return false;
		}

		RadioHost newHost = new RadioHost(updatedHost);

		await radioHostRepository.UpdateHost(newHost);
		return true;
	}

	public async Task<bool> DeleteHost(Guid id) {
		if (await this.GetHost(id) == null) {
			return false;
		}

		await radioHostRepository.DeleteHost(id);
		return true;
	}
}
