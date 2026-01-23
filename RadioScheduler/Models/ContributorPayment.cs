using RadioScheduler.Models.Auth;

namespace RadioScheduler.Models;

public class ContributorPayment {
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid UserId { get; set; }
	public Guid AccountingId { get; set; }
	public DateTime PaymentDate { get; set; }
	public decimal Amount { get; set; }
	public bool IsPaid { get; set; }

	public User User { get; set; } = null!;
	public Accounting Accounting { get; set; } = null!;
}
