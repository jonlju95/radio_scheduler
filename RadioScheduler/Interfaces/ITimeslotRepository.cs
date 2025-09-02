using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface ITimeslotRepository {
	IEnumerable<Timeslot> GetTimeslots();
	Timeslot? GetTimeslots(Guid id);
	Timeslot CreateTimeslot(Timeslot timeslot);
	void UpdateTimeslot(Timeslot existingTimeslot, Timeslot newTimeslot);
	void DeleteTimeslot(Timeslot timeslotToDelete);
}
