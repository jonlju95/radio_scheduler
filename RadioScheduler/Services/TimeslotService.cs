using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class TimeslotService(
	ITimeslotRepository timeslotRepository,
	IRadioHostRepository radioHostRepository,
	IRadioShowRepository radioShowRepository) {

	public IEnumerable<Timeslot> GetTimeslots() {
		return timeslotRepository.GetTimeslots();
	}

	public Timeslot? GetTimeslot(Guid id) {
		return timeslotRepository.GetTimeslots(id);
	}

	public Timeslot CreateTimeslot(Timeslot timeslot) {
		if (timeslot.Id == Guid.Empty) {
			timeslot.Id = Guid.NewGuid();
		}

		return timeslotRepository.CreateTimeslot(timeslot);
	}

	public bool UpdateTimeslot(Guid id, Timeslot timeslot) {
		Timeslot? existingTimeslot = timeslotRepository.GetTimeslots(id);
		if (existingTimeslot == null) {
			return false;
		}
		timeslotRepository.UpdateTimeslot(existingTimeslot, timeslot);
		return true;
	}

	public bool DeleteTimeslot(Guid id) {
		Timeslot? timeslot = this.GetTimeslot(id);
		if (timeslot == null) {
			return false;
		}
		timeslotRepository.DeleteTimeslot(timeslot);
		return true;
	}
}
