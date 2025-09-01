using RadioScheduler.Interfaces;
using RadioScheduler.Models;

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

	public void UpdateRadioTimeslot(RadioTimeslot radioTimeslot) {
		radioTimeslotRepository.UpdateRadioTimeslot(radioTimeslot);
	}

	public void DeleteRadioTimeslot(Guid id) {
		radioTimeslotRepository.DeleteRadioTimeslot(id);
	}
}
