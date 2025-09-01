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

	public void UpdateRadioHost(RadioHost radioHost) {
		RadioHost? existingRadioHost = this.GetRadioHost(radioHost.Id);
		if (existingRadioHost == null) {
			return;
		}

		existingRadioHost.FirstName = radioHost.FirstName;
		existingRadioHost.LastName = radioHost.LastName;
		existingRadioHost.IsGuest = radioHost.IsGuest;
	}

	public void DeleteRadioHost(Guid id) {
		RadioHost? radioHostToDelete = this.GetRadioHost(id);
		if (radioHostToDelete != null) {
			radioHosts.Remove(radioHostToDelete);
		}
	}
}
