using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface IRadioTimeslotRepository {
	IEnumerable<RadioTimeslot> GetRadioTimeslots();
	RadioTimeslot? GetRadioTimeslot(Guid id);
	RadioTimeslot CreateRadioTimeslot(RadioTimeslot radioTimeslot);
	void UpdateRadioTimeslot(RadioTimeslot radioTimeslot);
	void DeleteRadioTimeslot(Guid id);
}
