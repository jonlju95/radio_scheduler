using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class TableauService(ITableauRepository tableauRepository) {

	public async Task<IEnumerable<Tableau>?> GetTableaux(ApiResponse apiResponse) {
		IEnumerable<Tableau> tableaux = await tableauRepository.GetTableaux();

		if (tableaux != null) {
			return tableaux;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return tableaux;
	}

	public async Task<Tableau?> GetTableau(ApiResponse apiResponse, Guid id) {
		Tableau? tableau = await tableauRepository.GetTableau(id);

		if (tableau != null) {
			return tableau;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Tableau not found" });
		apiResponse.Success = false;

		return tableau;
	}

	public async Task<Tableau?> CreateTableau(ApiResponse apiResponse, Tableau tableau) {
		if (tableau == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Tableau data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (await this.GetTableau(apiResponse, tableau.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Tableau already exists" });
			apiResponse.Success = false;
			return null;
		}

		Tableau newTableau = new Tableau(tableau);

		await tableauRepository.CreateTableau(newTableau);
		return newTableau;
	}

	public async Task<bool> UpdateTableau(ApiResponse apiResponse, Guid id, Tableau updatedTableau) {
		if (updatedTableau == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Tableau data not provided" });
			apiResponse.Success = false;
			return false;
		}

		if (await this.GetTableau(apiResponse, id) == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Tableau not found" });
			apiResponse.Success = false;
			return false;
		}

		Tableau newTableau = new Tableau(updatedTableau);

		await tableauRepository.UpdateTableau(newTableau);
		return true;
	}

	public async Task<bool> DeleteTableau(ApiResponse apiResponse, Guid id) {
		if (await this.GetTableau(apiResponse, id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Tableau already exists" });
			apiResponse.Success = false;
			return false;
		}

		await tableauRepository.DeleteTableau(id);
		return true;
	}

	public async Task<List<Guid?>> CreateTableauForSchedule(Guid scheduleId, DateOnly startDate, DateOnly endDate) {
		List<Guid?> tableauList = [];
		for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1)) {
			// Tableau? newTableau = await tableauRepository.CreateTableau(new Tableau(Guid.NewGuid(), date, scheduleId));
			// tableauList.Add(newTableau?.Id);
		}

		return tableauList;
	}

	public async Task<Tableau?> GetDailyTableau(ApiResponse apiResponse, DateOnly date) {
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
