using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TimeslotRepository : ITimeslotRepository {
	private readonly List<Timeslot> timeslots = [];

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
		existingTimeslot.Start = newTimeslot.Start;
		existingTimeslot.End = newTimeslot.End;
		existingTimeslot.HostId = newTimeslot.HostId;
		existingTimeslot.ShowId = newTimeslot.ShowId;
	}

	public void DeleteTimeslot(Timeslot timeslotToDelete) {
		timeslots.Remove(timeslotToDelete);
	}
}
