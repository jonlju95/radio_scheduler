using System.ComponentModel.DataAnnotations.Schema;

namespace RadioScheduler.Models;

public class Studio {
	public Guid Id { get; init; } = Guid.NewGuid();
	public string Name { get; set; } = "";

	[Column("booking_price")] public decimal BookingPrice { get; set; }
	public int Capacity { get; set; }

	public Studio() {
	}

	public Studio(Guid id, string name, decimal bookingPrice, int capacity) {
		this.Id = id;
		this.Name = name;
		this.BookingPrice = bookingPrice;
		this.Capacity = capacity;
	}

	public Studio(Studio oldStudio) {
		this.Id = oldStudio.Id;
		this.Name = oldStudio.Name;
		this.BookingPrice = oldStudio.BookingPrice;
		this.Capacity = oldStudio.Capacity;
	}
}
