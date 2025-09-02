using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class RadioTimeslotService(
	IRadioTimeslotRepository radioTimeslotRepository,
	IRadioHostRepository radioHostRepository,
	IRadioShowRepository radioShowRepository) {

	public IEnumerable<RadioTimeslot> GetRadioTimeslots() {
		return radioTimeslotRepository.GetRadioTimeslots();
	}

	public RadioTimeslot? GetRadioTimeslot(Guid id) {
		return radioTimeslotRepository.GetRadioTimeslot(id);
	}

	public RadioTimeslot AddRadioTimeslot(RadioTimeslot radioTimeslot) {
		if (radioTimeslot.Id == Guid.Empty) {
			radioTimeslot.Id = Guid.NewGuid();
		}

		return radioTimeslotRepository.CreateRadioTimeslot(radioTimeslot);
	}

	public bool UpdateRadioTimeslot(Guid id, RadioTimeslot radioTimeslot) {
		RadioTimeslot? existingTimeslot = radioTimeslotRepository.GetRadioTimeslot(id);
		if (existingTimeslot == null) {
			return false;
		}
		radioTimeslotRepository.UpdateRadioTimeslot(existingTimeslot, radioTimeslot);
		return true;
	}

	public bool DeleteRadioTimeslot(Guid id) {
		RadioTimeslot? radioTimeslot = this.GetRadioTimeslot(id);
		if (radioTimeslot == null) {
			return false;
		}
		radioTimeslotRepository.DeleteRadioTimeslot(radioTimeslot);
		return true;
	}
}
