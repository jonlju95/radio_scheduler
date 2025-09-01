using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Utils;

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

	public void UpdateRadioShow(RadioShow radioShow) {
		RadioShow? existingRadioShow = this.GetRadioShow(radioShow.Id);
		if (existingRadioShow == null) {
			return;
		}

		existingRadioShow.Name = radioShow.Name;
		existingRadioShow.DurationMinutes = radioShow.DurationMinutes;
	}

	public void DeleteRadioShow(Guid id) {
		RadioShow? radioShowToDelete = this.GetRadioShow(id);
		if (radioShowToDelete != null) {
			radioShows.Remove(radioShowToDelete);
		}
	}
}
