using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

[ApiController]
[Route("/v1/[controller]s")]
public class RadioShowController(RadioShowService radioShowService) : Controller {

	[HttpGet]
	public ActionResult<List<RadioShow>> GetRadioShows() {
		return this.Ok(radioShowService.GetRadioShows());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<RadioShow> GetRadioShow(Guid id) {
		RadioShow? radioShow = radioShowService.GetRadioShow(id);
		return radioShow == null ? this.NotFound() : this.Ok(radioShow);
	}

	[HttpPost]
	public ActionResult<RadioShow> AddRadioShow(RadioShow radioShow) {
		RadioShow newRadioShow = radioShowService.AddRadioShow(radioShow);
		return this.CreatedAtAction(nameof(this.GetRadioShow), new { id = newRadioShow.Id }, newRadioShow);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<RadioShow> UpdateRadioShow(Guid id, RadioShow radioShow) {
		if (id != radioShow.Id) {
			return this.BadRequest();
		}
		radioShowService.UpdateRadioShow(radioShow);
		return this.NoContent();
	}

	[HttpDelete("{id:guid}")]
	public ActionResult DeleteRadioShow(Guid id) {
		radioShowService.DeleteRadioShow(id);
		return this.NoContent();
	}

}
