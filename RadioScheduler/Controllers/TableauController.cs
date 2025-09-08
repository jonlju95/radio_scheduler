using Microsoft.AspNetCore.Mvc;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;
using RadioScheduler.Services;

namespace RadioScheduler.Controllers;

public class TableauController(TableauService tableauService, ApiResponse apiResponse) : BaseApiController {

	[HttpGet]
	public async Task<IActionResult> GetTableaux() {
		apiResponse.Data = await tableauService.GetTableaux(apiResponse);

		return this.Ok(apiResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetTableau(Guid id) {
		apiResponse.Data = await tableauService.GetTableau(apiResponse, id);

		return this.Ok(apiResponse);
	}

	[HttpPost]
	public async Task<IActionResult> CreateTableau([FromBody] Tableau tableau) {
		apiResponse.Data = await tableauService.CreateTableau(apiResponse, tableau);

		return this.Ok(apiResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateTableau(Guid id, [FromBody] Tableau tableau) {
		apiResponse.Data = await tableauService.UpdateTableau(apiResponse, id, tableau);

		return this.Ok(apiResponse);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteTableau(Guid id) {
		apiResponse.Data = await tableauService.DeleteTableau(apiResponse, id);

		return this.Ok(apiResponse);
	}
}
