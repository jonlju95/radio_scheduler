using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Repositories;

public class TableauRepository : ITableauRepository {
	private readonly List<Tableau> tableaux = [];

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
}
