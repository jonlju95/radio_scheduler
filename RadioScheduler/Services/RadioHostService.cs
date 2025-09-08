using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class RadioHostService(IRadioHostRepository radioHostRepository) {

	public async Task<IEnumerable<RadioHost>> GetHosts(ApiResponse apiResponse) {
		IEnumerable<RadioHost> hosts = await radioHostRepository.GetHosts();

		if (hosts != null) {
			return hosts;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "List not found" });
		apiResponse.Success = false;

		return hosts;
	}

	public async Task<RadioHost?> GetHost(ApiResponse apiResponse, Guid id) {
		RadioHost? host = await radioHostRepository.GetHost(id);

		if (host != null) {
			return host;
		}

		apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Host not found" });
		apiResponse.Success = false;

		return host;
	}

	public async Task<RadioHost?> CreateHost(ApiResponse apiResponse, RadioHost host) {
		if (host == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Host data not provided" });
			apiResponse.Success = false;
			return null;
		}

		if (await this.GetHost(apiResponse, host.Id) != null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "CANCELLED", Message = "Host already exists" });
			apiResponse.Success = false;
			return null;
		}

		RadioHost newRadioHost = new RadioHost(host);

		await radioHostRepository.CreateHost(newRadioHost);
		return newRadioHost;
	}

	public async Task<bool> UpdateHost(ApiResponse apiResponse, Guid id, RadioHost host) {
		if (host == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "BAD_REQUEST", Message = "Host data not provided" });
			apiResponse.Success = false;
			return false;
		}

		RadioHost? existingHost = await this.GetHost(apiResponse, id);
		if (existingHost == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Host not found" });
			apiResponse.Success = false;
			return false;
		}

		RadioHost newHost = new RadioHost(host);

		await radioHostRepository.UpdateHost(newHost);
		return true;
	}

	public async Task<bool> DeleteHost(ApiResponse apiResponse, Guid id) {
		RadioHost? hostToDelete = await this.GetHost(apiResponse, id);
		if (hostToDelete == null) {
			apiResponse.Error.Add(new ErrorInfo { Code = "NOT_FOUND", Message = "Host not found" });
			apiResponse.Success = false;
			return false;
		}

		await radioHostRepository.DeleteHost(id);
		return true;
	}

	public async Task<IEnumerable<RadioHost>> GetMultipleHosts(ApiResponse apiResponse, List<RadioHost> timeslotHosts) {
		IEnumerable<RadioHost> multipleHosts = [];


		// multipleHosts.AddRange(timeslotHosts.Select(async radioHost => await this.GetHost(apiResponse, radioHost.Id)));
		return multipleHosts;
	}
}
