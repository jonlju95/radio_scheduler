using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class TableauController(TableauService tableauService) : BaseApiController {

	[HttpGet]
	public ActionResult<ResponseObject<List<Tableau>>> GetTableaux() {
		IEnumerable<Tableau> tableaux = tableauService.GetTableaux();

		return tableaux is null
			? this.FailResponse<List<Tableau>>("NOT_FOUND", $"List {tableaux} not found")
			: this.OkResponse(tableaux.ToList());
	}

	[HttpGet("{id:guid}")]
	public ActionResult<ResponseObject<Tableau>> GetTableau(Guid id) {
		Tableau? tableau = tableauService.GetTableau(id);

		return tableau is null
			? this.FailResponse<Tableau>("NOT_FOUND", $"Tableau {id} not found")
			: this.OkResponse(tableau);
	}

	[HttpPost]
	public ActionResult<ResponseObject<Tableau>> CreateTableau([FromBody] RequestObject<Tableau> request) {
		if (request.Data == null) {
			return this.FailResponse<Tableau>("BAD_REQUEST", $"No tableau data provided");
		}

		Tableau newTableau = tableauService.CreateTableau(request.Data);
		return this.OkResponse(newTableau);
	}

	[HttpPut("{id:guid}")]
	public ActionResult<ResponseObject<string>> UpdateTableau(Guid id, [FromBody] RequestObject<Tableau> request) {
		if (request.Data == null) {
			return this.FailResponse<string>("BAD_REQUEST", $"No tableau data provided");
		}

		bool success = tableauService.UpdateTableau(id, request.Data);
		return !success ?
			this.FailResponse<string>("NOT_FOUND", $"Tableau {id} not found") :
			this.OkResponse("Tableau updated");
	}

	[HttpDelete("{id:guid}")]
	public ActionResult<ResponseObject<string>> DeleteTableau(Guid id) {
		bool success = tableauService.DeleteTableau(id);
		return !success ?
			this.FailResponse<string>("NOT_FOUND", $"Tableau {id} not found") :
			this.OkResponse("Tableau deleted");
	}
}
