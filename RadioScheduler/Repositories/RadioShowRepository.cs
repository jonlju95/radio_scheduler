using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils.JsonReaders;

namespace RadioScheduler.Repositories;

public class RadioShowRepository : IRadioShowRepository {
	private readonly List<RadioShow> radioShows = RadioShowJsonReader.GetInMemoryRadioShows();

	public IEnumerable<RadioShow> GetRadioShows() {
		return radioShows;
	}

	public RadioShow? GetRadioShow(Guid id) {
		return radioShows.FirstOrDefault(x => x.Id == id);
	}

	public RadioShow CreateRadioShow(RadioShow radioShow) {
		if (radioShows.Contains(radioShow)) {
			return radioShow;
		}

		radioShows.Add(radioShow);
		return radioShow;
	}

	public void UpdateRadioShow(RadioShow existingRadioShow, RadioShow newRadioShow) {
		existingRadioShow.Name = newRadioShow.Name;
		existingRadioShow.DurationMinutes = newRadioShow.DurationMinutes;
	}

	public void DeleteRadioShow(RadioShow radioShowToDelete) {
		radioShows.Remove(radioShowToDelete);
	}
}
