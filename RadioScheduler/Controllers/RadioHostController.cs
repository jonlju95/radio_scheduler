using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class RadioHostController(RadioHostService radioHostService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<RadioHost>>> GetRadioHosts() {
		IEnumerable<RadioHost> radioHosts = radioHostService.GetRadioHosts();

		return radioHosts is null
			? this.FailResponse<List<RadioHost>>("NOT_FOUND", $"List {radioHosts} not found")
			: this.OkResponse(radioHosts.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<RadioHost>> GetRadioHost(Guid id) {
		RadioHost? radioHost = radioHostService.GetRadioHost(id);

		return radioHost is null
			? this.FailResponse<RadioHost>("NOT_FOUND", $"Radio host {id} not found")
			: this.OkResponse(radioHost);
	}

	[HttpPost]
	public ActionResult<ResponseObject<RadioHost>> AddRadioHost([FromBody] RequestObject<RadioHost> request) {
		if (request.Data == null) {
			return this.FailResponse<RadioHost>("BAD_REQUEST", "No radio host provided");
		}

		RadioHost newRadioHost = radioHostService.AddRadioHost(request.Data);
		return this.OkResponse(newRadioHost);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateRadioHost(Guid id, [FromBody] RequestObject<RadioHost> request) {
		if (request.Data == null) {
			return this.FailResponse<string>("BAD_REQUEST", "No radio host provided");
		}

		bool success = radioHostService.UpdateRadioHost(id, request.Data);
		return !success
			? this.FailResponse<string>("NOT_FOUND", $"Radio host {id} not found")
			: this.OkResponse("Radio host updated");
		;
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteRadioHost(Guid id) {
		bool success = radioHostService.DeleteRadioHost(id);
		return !success
			? this.FailResponse<string>("NOT_FOUND", $"Radio host {id} not found")
			: this.OkResponse("Radio host updated");
		;
	}
}
