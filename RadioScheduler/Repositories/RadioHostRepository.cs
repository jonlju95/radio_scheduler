using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class RadioHostRepository : IRadioHostRepository {
	private readonly List<RadioHost> radioHosts = RadioHostJsonReader.GetInMemoryRadioHosts();

	public IEnumerable<RadioHost> GetRadioHosts() {
		return radioHosts;
	}

	public RadioHost? GetRadioHost(Guid id) {
		return radioHosts.FirstOrDefault(x => x.Id == id);
	}

	public RadioHost CreateRadioHost(RadioHost radioHost) {
		if (radioHosts.Contains(radioHost)) {
			return radioHost;
		}

		radioHosts.Add(radioHost);
		return radioHost;
	}

	public void UpdateRadioHost(RadioHost existingRadioHost, RadioHost newRadioHost) {
		existingRadioHost.FirstName = newRadioHost.FirstName;
		existingRadioHost.LastName = newRadioHost.LastName;
		existingRadioHost.IsGuest = newRadioHost.IsGuest;
	}

	public void DeleteRadioHost(RadioHost radioHostToDelete) {
		radioHosts.Remove(radioHostToDelete);
	}
}
