using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class TableauService(
	ITableauRepository tableauRepository,
	RadioHostService radioHostService,
	RadioShowService radioShowService,
	StudioService studioService) {

	public IEnumerable<Tableau> GetTableaux() {
		return tableauRepository.GetTableaux();
	}

	public Tableau? GetTableau(Guid id) {
		return tableauRepository.GetTableau(id);
	}

	public Tableau CreateTableau(Tableau tableau) {
		Tableau newTableau = new Tableau(Guid.NewGuid(), tableau.Date);
		return tableauRepository.CreateTableau(newTableau);
	}

	public bool UpdateTableau(Guid id, Tableau updatedTableau) {
		Tableau? existingTableau = this.GetTableau(id);
		if (existingTableau == null) {
			return false;
		}

		Tableau newTableau = new Tableau(updatedTableau) {
			Date = updatedTableau.Date,
			Timeslots = updatedTableau.Timeslots,
		};

		tableauRepository.UpdateTableau(existingTableau, newTableau);
		return true;
	}

	public bool DeleteTableau(Guid id) {
		Tableau? tableau = this.GetTableau(id);
		if (tableau == null) {
			return false;
		}

		tableauRepository.DeleteTableau(tableau);
		return true;
	}

	public List<Tableau> CreateTableauForSchedule(DateOnly startDate, DateOnly endDate) {
		List<Tableau> tableauList = [];
		for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1)) {
			tableauList.Add(tableauRepository.CreateTableau(new Tableau(Guid.NewGuid(), date)));
		}

		return tableauList;
	}

	public Tableau? GetDailyTableau(DateOnly date) {
		Tableau? tableau = tableauRepository.GetDailyTableau(date);

		if (tableau?.Timeslots == null) {
			return tableau;
		}

		foreach (Timeslot tableauTimeslot in tableau.Timeslots) {
			if (tableauTimeslot.Hosts is { Count: > 0 }) {
				tableauTimeslot.Hosts = radioHostService.GetMultipleHosts(tableauTimeslot.Hosts);
			}

			if (tableauTimeslot.Show != null) {
				tableauTimeslot.Show = radioShowService.GetRadioShow(tableauTimeslot.Show.Id);
			}

			if (tableauTimeslot.Studio != null) {
				tableauTimeslot.Studio = studioService.GetStudio(tableauTimeslot.Studio.Id);
			}
		}

		return tableau;
	}
}
