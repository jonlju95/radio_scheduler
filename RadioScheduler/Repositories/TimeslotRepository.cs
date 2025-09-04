using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TimeslotRepository(List<Timeslot> timeslots) : ITimeslotRepository {

	public IEnumerable<Timeslot> GetTimeslots() {
		return timeslots;
	}

	public Timeslot? GetTimeslots(Guid id) {
		return timeslots.FirstOrDefault(x => x.Id == id);
	}

	public Timeslot CreateTimeslot(Timeslot timeslot) {
		if (timeslots.Contains(timeslot)) {
			return timeslot;
		}

		timeslots.Add(timeslot);
		return timeslot;
	}

	public void UpdateTimeslot(Timeslot existingTimeslot, Timeslot newTimeslot) {
		int index = timeslots.IndexOf(existingTimeslot);
		timeslots[index] = newTimeslot;
	}

	public void DeleteTimeslot(Timeslot timeslotToDelete) {
		timeslots.Remove(timeslotToDelete);
	}
}
