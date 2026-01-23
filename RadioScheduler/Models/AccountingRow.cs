namespace RadioScheduler.Models;

public class AccountingRow {
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid AccountingId { get; set; }
	public string AccountNumber { get; set; } = string.Empty;
	public string AccountName { get; set; } = string.Empty;
	public int Vat { get; set; }
	public decimal Credit { get; set; }
	public decimal Debit { get; set; }
	public Accounting Accounting { get; set; } = null!;
}
