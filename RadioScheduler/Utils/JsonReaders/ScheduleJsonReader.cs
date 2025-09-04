using System.Text.Json;
using RadioScheduler.Models;
using RadioScheduler.Services;

namespace RadioScheduler.Utils.JsonReaders;

public static class ScheduleJsonReader {

	private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions {
		PropertyNameCaseInsensitive = true
	};

	public static List<Schedule> GetInMemorySchedules() {
		try {
			using StreamReader streamReader = new StreamReader("./Data/Schedule.json");
			string json = streamReader.ReadToEnd();

			List<Schedule> schedules = JsonSerializer.Deserialize<List<Schedule>>(json, jsonSerializerOptions) ?? [];
			if (schedules.Count > 0) {
				schedules.ForEach(schedule => {
					foreach (Tableau tableau in schedule.Tableaux) {
						for (int hour = 0; hour < 24; hour += 2) {
							Timeslot timeslot = new Timeslot {
								Start = new TimeOnly(hour, 0),
								End = new TimeOnly(Math.Min(hour + 2, 23), Math.Min(hour + 2, 23) == 23 ? 59 : 0),
								Hosts = [new RadioHost { Id = Guid.Parse("f40dd0a4-5e9c-4c2b-8b29-212c1b7dc8a1") }],
								Show = new RadioShow { Id = Guid.Parse("1d64740a-7327-40fc-ad82-7fdb6d6a0af5") },
								Studio = new Studio { Id = Guid.Parse("fbb3a4a4-48df-4b82-af48-0ccb2f10854a") }
							};

							tableau.Timeslots.Add(timeslot);
						}
					}
				});
			}

			return schedules;
		} catch (Exception e) {
			return [];
		}
	}

	private static void FillTimeslots(Schedule schedule) {
		// foreach (Tableau tableau in schedule.Tableaux) {
		// 	TimeOnly startTime = new TimeOnly(0, 0);
		//
		// 	while (startTime < new TimeOnly(24, 00)) {
		// 		TimeOnly endTime = startTime.AddHours(2);
		//
		// 		Timeslot timeslot = new Timeslot {
		// 			Start = startTime,
		// 			End = endTime,
		// 			Hosts = [Guid.Parse("f40dd0a4-5e9c-4c2b-8b29-212c1b7dc8a1")],
		// 			Show = Guid.Parse("1d64740a-7327-40fc-ad82-7fdb6d6a0af5"),
		// 			Studio = Guid.Parse("fbb3a4a4-48df-4b82-af48-0ccb2f10854a")
		// 		};
		//
		// 		tableau.Timeslots.Add(timeslot);
		//
		// 		startTime = endTime;
		// 	}
		// }
	}
}
