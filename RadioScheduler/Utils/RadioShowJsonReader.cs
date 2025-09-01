using System.Text.Json;
using RadioScheduler.Models;

namespace RadioScheduler.Utils;

public static class RadioShowJsonReader {

	// Method to populate the in-memory list with predefined radio shows
	public static List<RadioShow> GetInMemoryRadioShows() {
		try {
			using StreamReader r = new StreamReader("./Data/RadioShows.json");
			string json = r.ReadToEnd();

			List<RadioShow> radioShows = (JsonSerializer.Deserialize<List<RadioShow>>(json) ?? []).Select(show =>
				new RadioShow {
					Id = show.Id,
					Name = show.Name,
					DurationMinutes = show.DurationMinutes,
				}).ToList();

			return radioShows;
		} catch (Exception e) {
			return [];
		}
	}
}
