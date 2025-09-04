using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TableauRepository(List<Tableau> tableaux) : ITableauRepository {

	public IEnumerable<Tableau> GetTableaux() {
		return tableaux;
	}

	public Tableau? GetTableau(Guid id) {
		return tableaux.FirstOrDefault(t => t.Id == id);
	}

	public Tableau CreateTableau(Tableau tableau) {
		if (tableaux.Contains(tableau)) {
			return tableau;
		}

		tableaux.Add(tableau);
		return tableau;
	}

	public void UpdateTableau(Tableau existingTableau, Tableau newTableau) {
		int index = tableaux.IndexOf(existingTableau);
		tableaux[index] = newTableau;
	}

	public void DeleteTableau(Tableau tableauToDelete) {
		tableaux.Remove(tableauToDelete);
	}

	public Tableau? GetDailyTableau(DateOnly date) {
		return tableaux.FirstOrDefault(t => t.Date == date);
	}
}
