using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioHostRepository : IRadioHostRepository {
	private readonly List<RadioHost> radioHosts = RadioHostJsonReader.GetInMemoryRadioHosts();

	public IEnumerable<RadioHost> GetHosts() {
		return radioHosts;
	}

	public RadioHost? GetHost(Guid id) {
		return radioHosts.FirstOrDefault(x => x.Id == id);
	}

	public RadioHost CreateHost(RadioHost host) {
		if (radioHosts.Contains(host)) {
			return host;
		}

		radioHosts.Add(host);
		return host;
	}

	public void UpdateHost(RadioHost existingHost, RadioHost newHost) {
		existingHost.FirstName = newHost.FirstName;
		existingHost.LastName = newHost.LastName;
		existingHost.IsGuest = newHost.IsGuest;
	}

	public void DeleteHost(RadioHost hostToDelete) {
		radioHosts.Remove(hostToDelete);
	}
}
