using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
public class RadioTimeslotController(RadioTimeslotService radioTimeslotService) : Controller {

	[HttpGet]
	public ActionResult<List<RadioTimeslot>> GetRadioTimeslots() {
		return this.Ok(radioTimeslotService.GetRadioTimeslots());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<RadioTimeslot> GetRadioTimeslot(Guid id) {
		RadioTimeslot? radioTimeslot = radioTimeslotService.GetRadioTimeslot(id);
		return radioTimeslot is null ? this.NotFound() : this.Ok(radioTimeslot);
	}

	[HttpPost]
	public ActionResult<RadioTimeslot> AddRadioTimeslot(RadioTimeslot radioTimeslot) {
		RadioTimeslot newRadioTimeslot = radioTimeslotService.AddRadioTimeslot(radioTimeslot);
		return this.CreatedAtAction(nameof(this.GetRadioTimeslot), new { id = newRadioTimeslot.Id }, newRadioTimeslot);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<RadioTimeslot> UpdateRadioTimeslot(Guid id, RadioTimeslot radioTimeslot) {
		if (id != radioTimeslot.Id) {
			return this.BadRequest();
		}

		radioTimeslotService.UpdateRadioTimeslot(radioTimeslot);
		return this.NoContent();
	}

	[HttpDelete("{id:guid}")]
	public ActionResult DeleteRadioTimeslot(Guid id) {
		radioTimeslotService.DeleteRadioTimeslot(id);
		return this.NoContent();
	}
}
