using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

[Route("/[controller]x")]
public class TableauController(TableauService tableauService, ApiResponse apiResponse)
	: BaseApiController(apiResponse) {

	[HttpGet]
	public async Task<ActionResult<ApiResponse>> GetTableaux() {
		IEnumerable<Tableau> tableaux = await tableauService.GetTableaux();

		return tableaux == null || !tableaux.Any() ? this.NotFoundResponse() : this.SuccessResponse(tableaux);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> GetTableau(Guid id) {
		Tableau? tableau = await tableauService.GetTableau(id);

		return tableau == null ? this.NotFoundResponse() : this.SuccessResponse(tableau);
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse>> CreateTableau([FromBody] Tableau tableau) {
		if (tableau == null) {
			return this.BadRequestResponse();
		}

		Tableau? newTableau = await tableauService.CreateTableau(tableau);

		return newTableau == null
			? this.ConflictResponse("Tableau")
			: this.CreatedResponse(nameof(tableau), tableau, newTableau);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> UpdateTableau(Guid id, [FromBody] Tableau tableau) {
		if (tableau == null) {
			return this.BadRequestResponse();
		}

		return await tableauService.UpdateTableau(id, tableau)
			? this.SuccessResponse(tableau)
			: this.NoContentResponse();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult<ApiResponse>> DeleteTableau(Guid id) {
		return await tableauService.DeleteTableau(id)
			? this.SuccessResponse(id)
			: this.NoContentResponse();
	}

	[HttpGet("{id:guid}/timeslots")]
	public async Task<ActionResult<ApiResponse>> GetTableauWithTimeslots(Guid id) {
		Tableau? tableau = await tableauService.GetTableauWithTimeslots(id);

		return tableau == null ? this.NotFoundResponse() : this.SuccessResponse(tableau);
	}

	[HttpGet("daily")]
	public async Task<ActionResult<ApiResponse>> GetDailyTableau([FromQuery] DateOnly date) {
		Tableau? tableau = await tableauService.GetDailyTableau(date);

		return tableau == null ? this.NotFoundResponse() : this.SuccessResponse(tableau);
	}

	[HttpGet("weekly")]
	public async Task<ActionResult<ApiResponse>> GetWeeklyTableau([FromQuery] DateOnly date) {
		IEnumerable<Tableau> tableaux = await tableauService.GetWeeklyTableau(date);

		return tableaux == null || !tableaux.Any() ? this.NotFoundResponse() : this.SuccessResponse(tableaux);
	}
}
