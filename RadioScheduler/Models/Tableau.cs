using RadioScheduler.Models.Base;
using RadioScheduler.Utils;

namespace RadioScheduler.Models;

public class Tableau {
	public Guid Id { get; init; } = Guid.NewGuid();
	public Timestamp Date { get; init; } = Timestamp.CurrentTimestamp();
	public Guid ScheduleId { get; init; }
	public List<Guid> TimeslotIds { get; set; } = [];

	public Tableau() {
	}

	public Tableau(Guid id, Timestamp date, Guid scheduleId) {
		this.Id = id;
		this.Date = date;
		this.ScheduleId = scheduleId;
	}
}
