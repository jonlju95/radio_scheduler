using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Models.Base;
using RadioScheduler.Utils;

namespace RadioScheduler.Services;

public class TableauService(ITableauRepository tableauRepository) {

	public async Task<IEnumerable<Tableau>> GetTableaux() {
		return await tableauRepository.GetTableaux();
	}

	public async Task<Tableau?> GetTableau(Guid id) {
		return await tableauRepository.GetTableau(id);
	}

	public async Task<Tableau?> CreateTableau(Tableau tableau) {
		if (await this.GetTableau(tableau.Id) != null) {
			return null;
		}

		Tableau newTableau = new Tableau(tableau.Id, tableau.Date, tableau.ScheduleId);

		await tableauRepository.CreateTableau(newTableau);
		return newTableau;
	}

	public async Task<bool> UpdateTableau(Guid id, Tableau updatedTableau) {
		if (await this.GetTableau(id) == null) {
			return false;
		}

		Tableau newTableau = new Tableau(updatedTableau.Id, updatedTableau.Date, updatedTableau.ScheduleId);

		await tableauRepository.UpdateTableau(newTableau);
		return true;
	}

	public async Task<bool> DeleteTableau(Guid id) {
		if (await this.GetTableau(id) != null) {
			return false;
		}

		await tableauRepository.DeleteTableau(id);
		return true;
	}

	public async Task<List<Guid?>> CreateTableauForSchedule(Guid scheduleId, int year, int month) {
		List<Guid?> tableauList = [];
		for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++) {
			Tableau newTableau = new Tableau(Guid.NewGuid(), Timestamp.FromDateTimeUTC(new DateTime(year, month, day)), scheduleId);
			await tableauRepository.CreateTableau(newTableau);
			tableauList.Add(newTableau.Id);
		}

		return tableauList;
	}

	public async Task<Tableau?> GetDailyTableau(DateOnly date) {
		Tableau? tableau = await tableauRepository.GetDailyTableau(date);

		if (tableau?.TimeslotIds == null) {
			return tableau;
		}

		// foreach (Guid tableauTimeslot in tableau.TimeslotIds) {
		// 	if (tableauTimeslot.HostIds is { Count: > 0 }) {
		// 		// tableauTimeslot.HostIds = radioHostService.GetMultipleHosts(tableauTimeslot.HostIds);
		// 	}
		//
		// 	if (tableauTimeslot.ShowId != null) {
		// 		// tableauTimeslot.ShowId = radioShowService.GetRadioShow(tableauTimeslot.ShowId.Id);
		// 	}
		//
		// 	if (tableauTimeslot.StudioId != null) {
		// 		// tableauTimeslot.StudioId = studioService.GetStudio(tableauTimeslot.StudioId.Id);
		// 	}
		// }

		return tableau;
	}
}
