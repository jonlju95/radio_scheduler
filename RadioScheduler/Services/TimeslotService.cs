using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class TimeslotService(ITimeslotRepository timeslotRepository) {

	public async Task<IEnumerable<Timeslot>> GetTimeslots(ApiResponse apiResponse) {
		IEnumerable<Timeslot> timeslots = await timeslotRepository.GetTimeslots();

		if (timeslots != null) {
			return timeslots;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return timeslots;
	}

	public async Task<Timeslot?> GetTimeslot(ApiResponse apiResponse, Guid id) {
		Timeslot? timeslot = await timeslotRepository.GetTimeslot(id);

		if (timeslot != null) {
			return timeslot;
		}
		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Timeslot not found" });
		apiResponse.Success = false;

		return timeslot;
	}

	public async Task<Timeslot?> CreateTimeslot(ApiResponse apiResponse, Timeslot timeslot) {
		if (timeslot == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Timeslot data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (await this.GetTimeslot(apiResponse, timeslot.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Timeslot already exists" });
			apiResponse.Success = false;
			return null;
		}

		Timeslot newTimeslot = new Timeslot(timeslot);

		await timeslotRepository.CreateTimeslot(newTimeslot);
		return newTimeslot;
	}

	public async Task<bool> UpdateTimeslot(ApiResponse apiResponse, Guid id, Timeslot updatedTimeslot) {
		if (updatedTimeslot == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Timeslot data not provided" });
			apiResponse.Success = false;
			return false;
		}

		if (await this.GetTimeslot(apiResponse, id) == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Timeslot not found" });
			apiResponse.Success = false;
			return false;
		}

		Timeslot newTimeslot = new Timeslot(updatedTimeslot);

		await timeslotRepository.UpdateTimeslot(newTimeslot);
		return true;
	}

	public async Task<bool> DeleteTimeslot(ApiResponse apiResponse, Guid id) {
		if (await this.GetTimeslot(apiResponse, id) == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Timeslot not found" });
			apiResponse.Success = false;
			return false;
		}

		await timeslotRepository.DeleteTimeslot(id);
		return true;
	}
}
