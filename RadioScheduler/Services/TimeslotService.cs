using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class TimeslotService(
	ITimeslotRepository timeslotRepository,
	RadioHostService radioHostService,
	RadioShowService radioShowService) {

	public IEnumerable<Timeslot> GetTimeslots() {
		return timeslotRepository.GetTimeslots();
	}

	public Timeslot? GetTimeslot(Guid id) {
		return timeslotRepository.GetTimeslots(id);
	}

	public Timeslot CreateTimeslot(Timeslot timeslot) {
		Timeslot newTimeslot = new Timeslot(timeslot);
		return timeslotRepository.CreateTimeslot(newTimeslot);
	}

	public bool UpdateTimeslot(Guid id, Timeslot updatedTimeslot) {
		Timeslot? existingTimeslot = timeslotRepository.GetTimeslots(id);
		if (existingTimeslot == null) {
			return false;
		}

		Timeslot newTimeslot = new Timeslot(existingTimeslot) {
			StartTime = updatedTimeslot.StartTime,
			EndTime = updatedTimeslot.EndTime,
			Show = updatedTimeslot.Show,
			Hosts = updatedTimeslot.Hosts,
			Studio = updatedTimeslot.Studio
		};

		timeslotRepository.UpdateTimeslot(existingTimeslot, newTimeslot);
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
