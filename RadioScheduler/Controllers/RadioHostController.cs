using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/[controller]s")]
public class RadioHostController(RadioHostService radioHostService) : Controller {

	[HttpGet]
	public ActionResult<List<RadioHost>> GetRadioHosts() {
		return this.Ok(radioHostService.GetRadioHosts());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<RadioHost> GetRadioHost(Guid id) {
		RadioHost? radioHost = radioHostService.GetRadioHost(id);
		return radioHost is null ? this.NotFound() : this.Ok(radioHost);
	}

	[HttpPost]
	public ActionResult<RadioHost> AddRadioHost(RadioHost radioHost) {
		RadioHost newRadioHost = radioHostService.AddRadioHost(radioHost);
		return this.CreatedAtAction(nameof(this.GetRadioHost), new { id = newRadioHost.Id }, newRadioHost);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<RadioHost> UpdateRadioHost(Guid id, RadioHost radioHost) {
		if (id != radioHost.Id) {
			return this.BadRequest();
		}

		radioHostService.UpdateRadioHost(radioHost);
		return this.NoContent();
	}

	[HttpDelete("{id:guid}")]
	public ActionResult DeleteRadioHost(Guid id) {
		radioHostService.DeleteRadioHost(id);
		return this.NoContent();
	}
}
