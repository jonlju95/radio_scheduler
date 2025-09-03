using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface ITableauRepository {
	IEnumerable<Tableau> GetTableaux();
	Tableau? GetTableau(Guid id);
	Tableau CreateTableau(Tableau tableau);
	void UpdateTableau(Tableau existingTableau, Tableau newTableau);
	void DeleteTableau(Tableau tableauToDelete);
}
