using System.Text.Json;
using RadioScheduler.Models;

namespace RadioScheduler.Utils.JsonReaders;

public static class StudioJsonReader {

	public static List<Studio> GetInMemoryStudios() {
		try {
			using StreamReader streamReader = new StreamReader("./Data/Studios.json");
			string json = streamReader.ReadToEnd();

			List<Studio> studios = (JsonSerializer.Deserialize<List<Studio>>(json) ?? [])
				.Select(studio =>
					new Studio {
						Id = studio.Id,
						Name = studio.Name,
						BookingPrice = studio.BookingPrice,
						Capacity = studio.Capacity,
					}).ToList();
			return studios;
		} catch (Exception e) {
			return [];
		}
	}
}
