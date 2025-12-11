namespace RadioScheduler.Models;

public class Accounting {
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
	public string AccountNumber { get; set; } = string.Empty;
	public string AccountName { get; set; } = string.Empty;
	public int VAT { get; set; } = 0;
	public decimal Credit { get; set; } = 0;
	public decimal Debit { get; set; } = 0;
	public ContributorPayment ContributorPayment { get; set; } = null!;
}
