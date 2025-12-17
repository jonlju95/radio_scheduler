namespace RadioScheduler.Models;

public class Accounting {
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
	public ContributorPayment ContributorPayment { get; set; } = null!;
	public ICollection<AccountingRow> AccountingRows { get; set; } = new List<AccountingRow>();
}
