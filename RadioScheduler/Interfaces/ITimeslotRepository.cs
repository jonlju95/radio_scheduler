using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface ITimeslotRepository {
	Task<IEnumerable<Timeslot>> GetTimeslots();
	Task<Timeslot?> GetTimeslot(Guid id);
	Task CreateTimeslot(Timeslot timeslot);
	Task UpdateTimeslot(Timeslot newTimeslot);
	Task DeleteTimeslot(Guid id);
}
