using RadioScheduler.Models;

namespace RadioScheduler.Interfaces;

public interface ITableauRepository {
	Task<IEnumerable<Tableau>> GetTableaux();
	Task<Tableau?> GetTableau(Guid id);
	Task<Tableau?> GetDailyTableau(DateOnly date);
	Task CreateTableau(Tableau tableau);
	Task UpdateTableau(Tableau newTableau);
	Task DeleteTableau(Guid id);
	Task<IEnumerable<Tableau>> GetWeeklyTableaux(DateOnly startDate, DateOnly endDate);
}
