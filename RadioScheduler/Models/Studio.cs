namespace RadioScheduler.Models;

public class Studio {
	public Guid Id { get; init; }
	public string Name { get; set; } = "";
	public decimal BookingPrice { get; set; }
	public int Capacity { get; set; }

	public Studio() {}

	public Studio(Studio oldStudio) {
		this.Id = oldStudio.Id;
		this.Name = oldStudio.Name;
		this.BookingPrice = oldStudio.BookingPrice;
		this.Capacity = oldStudio.Capacity;
	}
}
