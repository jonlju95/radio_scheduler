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

	public void UpdateRadioTimeslot(RadioTimeslot radioTimeslot) {
		RadioTimeslot? existingRadioTimeslot = this.GetRadioTimeslot(radioTimeslot.Id);
		if (existingRadioTimeslot == null) {
			return;
		}

		existingRadioTimeslot.Start = radioTimeslot.Start;
		existingRadioTimeslot.End = radioTimeslot.End;
		existingRadioTimeslot.HostId = radioTimeslot.HostId;
		existingRadioTimeslot.ShowId = radioTimeslot.ShowId;
	}

	public void DeleteRadioTimeslot(Guid id) {
		RadioTimeslot? radioTimeslot = this.GetRadioTimeslot(id);
		if (radioTimeslot != null) {
			radioTimeslots.Remove(radioTimeslot);
		}
	}
}
