using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface ITimeslotRepository {
	Task<IEnumerable<Timeslot>> GetTimeslots();
	Task<Timeslot?> GetTimeslot(Guid id);
	Task CreateTimeslot(Timeslot timeslot);
	Task UpdateTimeslot(Timeslot newTimeslot);
	Task DeleteTimeslot(Guid id);
	Task<IEnumerable<Timeslot>> GetTimeslotByTableauId(Guid id);
	Task CreateHostTimeslotConnection(Guid timeslotId, Guid hostId);
	Task DeleteHostTimeslotConnection(Guid timeslotId, Guid hostId);
}
