using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class RadioTimeslotRepository : IRadioTimeslotRepository {
	private readonly List<RadioTimeslot> radioTimeslots = [];

	public IEnumerable<RadioTimeslot> GetRadioTimeslots() {
		return radioTimeslots;
	}

	public RadioTimeslot? GetRadioTimeslot(Guid id) {
		return radioTimeslots.FirstOrDefault(x => x.Id == id);
	}

	public RadioTimeslot CreateRadioTimeslot(RadioTimeslot radioTimeslot) {
		if (radioTimeslots.Contains(radioTimeslot)) {
			return radioTimeslot;
		}

		radioTimeslots.Add(radioTimeslot);
		return radioTimeslot;
	}

	public void UpdateRadioTimeslot(RadioTimeslot existingRadioTimeslot, RadioTimeslot newRadioTimeslot) {
		existingRadioTimeslot.Start = newRadioTimeslot.Start;
		existingRadioTimeslot.End = newRadioTimeslot.End;
		existingRadioTimeslot.HostId = newRadioTimeslot.HostId;
		existingRadioTimeslot.ShowId = newRadioTimeslot.ShowId;
	}

	public void DeleteRadioTimeslot(RadioTimeslot radioTimeslotToDelete) {
		radioTimeslots.Remove(radioTimeslotToDelete);
	}
}
