using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RadioScheduler.Models;

public class Timeslot {
	public Guid Id { get; set; } = Guid.NewGuid();

	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }

	public Guid TableauId { get; set; }

	[JsonIgnore]
	public Tableau? Tableau { get; set; } = null!;

	public ICollection<Guid> RadioHostIds { get; set; } = new List<Guid>();
	public ICollection<RadioHost> RadioHosts { get; set; } = new List<RadioHost>();

	public Guid? RadioShowId { get; set; }
	public RadioShow? RadioShow { get; set; }

	public Guid? StudioId { get; set; }
	public Studio? Studio { get; set; }

	public Timeslot() {
	}

	public Timeslot(Guid id, DateTime startTime, DateTime endTime, Guid tableauId,
		Guid? showId, Guid? studioId) {
		this.Id = id;
		this.StartTime = startTime;
		this.EndTime = endTime;
		this.TableauId = tableauId;
		this.RadioShowId = showId;
		this.StudioId = studioId;
	}
}
