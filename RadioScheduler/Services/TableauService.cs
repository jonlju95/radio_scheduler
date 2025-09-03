using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class TableauService(ITableauRepository tableauRepository) {

	public IEnumerable<Tableau> GetTableaux() {
		return tableauRepository.GetTableaux();
	}

	public Tableau? GetTableau(Guid id) {
		return tableauRepository.GetTableau(id);
	}

	public Tableau CreateTableau(Tableau tableau) {
		Tableau newTableau = new Tableau(tableau);
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
}
