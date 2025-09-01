using System.Text.Json;
using RadioScheduler.Models;

namespace RadioScheduler.Utils;

public static class RadioHostJsonReader {

	// Method to populate the in-memory list with predefined radio hosts and guests
	public static List<RadioHost> GetInMemoryRadioHosts() {
		try {
			using StreamReader r = new StreamReader("./Data/RadioHosts.json");
			string json = r.ReadToEnd();

			List<RadioHost> radioHosts = (JsonSerializer.Deserialize<List<RadioHost>>(json) ?? []).Select(host =>
				new RadioHost {
					Id = host.Id,
					FirstName = host.FirstName,
					LastName = host.LastName,
					IsGuest = host.IsGuest,
				}).ToList();

			return radioHosts;
		} catch (Exception e) {
			return [];
		}
	}
}
