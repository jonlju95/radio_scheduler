using System.Security.Cryptography;
using System.Text.Json;
using RadioScheduler.Models;
using RadioScheduler.Services;

namespace RadioScheduler.Utils.JsonReaders;

public static class ScheduleJsonReader {

	private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
		PropertyNameCaseInsensitive = true
	};

	private static readonly Guid[] radioHostIds = [
		Guid.Parse("f40dd0a4-5e9c-4c2b-8b29-212c1b7dc8a1"), Guid.Parse("1deb3b15-63f2-48a7-b8b5-ce3648018b0a"),
		Guid.Parse("7821040e-802f-4adf-81ae-38e32f628adb")
	];

	private static readonly Guid[] radioGuestIds = [
		Guid.Parse("73f97256-6423-4815-a330-58f33ca1b050"), Guid.Parse("f734e369-8b2d-42fa-b44a-7185170f4ba2"),
		Guid.Parse("8e09fc5a-3042-4328-bcd3-0e805297425f")
	];

	private static readonly Guid[] radioShowIds = [
		Guid.Parse("1d64740a-7327-40fc-ad82-7fdb6d6a0af5"), Guid.Parse("9c828a05-9849-4d86-8028-a9b139592e6a"),
		Guid.Parse("7d504caa-b4a8-404a-8af0-1fda5a902925")
	];

	public static List<Schedule> GetInMemorySchedules() {
		try {
			using StreamReader streamReader = new StreamReader("./Data/Schedule.json");
			string json = streamReader.ReadToEnd();

			List<Schedule> schedules = JsonSerializer.Deserialize<List<Schedule>>(json, jsonSerializerOptions) ?? [];
			if (schedules.Count > 0) {
				schedules.ForEach(schedule => {
					foreach (Tableau tableau in schedule.Tableaux) {
						for (int hour = 0; hour < 24; hour += 2) {
							tableau.Timeslots.Add(GenerateTimeslot(hour));
						}
					}
				});
			}

			return schedules;
		} catch (Exception e) {
			return [];
		}
	}

	private static Timeslot GenerateTimeslot(int hour) {
		Random rnd = new Random();

		List<RadioHost> randomHosts = GenerateRadioHosts(rnd);

		return new Timeslot {
			Start = new TimeOnly(hour, 0),
			End = new TimeOnly(Math.Min(hour + 2, 23), Math.Min(hour + 2, 23) == 23 ? 59 : 0),
			Hosts = randomHosts,
			Show = new RadioShow { Id = radioShowIds[rnd.Next(0, 3)] },
			Studio = new Studio {
				Id = randomHosts.Count > 1
					? Guid.Parse("5540e85d-da1e-407b-b0e1-cd315ab74ac6")
					: Guid.Parse("fbb3a4a4-48df-4b82-af48-0ccb2f10854a")
			}
		};
	}

	private static List<RadioHost> GenerateRadioHosts(Random rnd) {
		List<RadioHost> randomHosts = [
			new RadioHost { Id = radioHostIds[rnd.Next(0, 3)] }
		];

		if (rnd.Next(1, 3) == 2) {
			randomHosts.Add(new RadioHost { Id = radioGuestIds[rnd.Next(0, 3)] });
		}

		return randomHosts;
	}
}
