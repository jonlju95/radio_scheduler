using Dapper;
using RadioScheduler.Interfaces;
using RadioScheduler.Models;

namespace RadioScheduler.Services;

public class TimeslotService(
	ITimeslotRepository timeslotRepository,
	IRadioHostRepository radioHostRepository,
	IRadioShowRepository radioShowRepository,
	IStudioRepository studioRepository) {

	public async Task<IEnumerable<Timeslot>> GetTimeslots() {
		return await timeslotRepository.GetTimeslots();
	}

	public async Task<Timeslot?> GetTimeslot(Guid id) {
		Timeslot? timeslot = await timeslotRepository.GetTimeslot(id);

		return timeslot;
	}

	public async Task<Timeslot?> CreateTimeslot(Timeslot timeslot) {
		if (await this.GetTimeslot(timeslot.Id) != null) {
			return null;
		}

		Timeslot newTimeslot = new Timeslot(timeslot.Id, timeslot.StartTime, timeslot.EndTime, timeslot.TableauId, timeslot.RadioShowId, timeslot.StudioId);

		await timeslotRepository.CreateTimeslot(newTimeslot);

		newTimeslot.RadioHosts = this.AddRadioHosts(newTimeslot.Id, timeslot.RadioHostIds);

		if (newTimeslot.RadioShow != null) {
			newTimeslot.RadioShow = await radioShowRepository.GetRadioShow(newTimeslot.RadioShow.Id);
		}

		if (newTimeslot.Studio != null) {
			newTimeslot.Studio = await studioRepository.GetStudio(newTimeslot.Studio.Id);
		}

		return newTimeslot;
	}

	public async Task<bool> UpdateTimeslot(Guid id, Timeslot updatedTimeslot) {
		if (await this.GetTimeslot(id) == null) {
			return false;
		}

		Timeslot newTimeslot = new Timeslot(id, updatedTimeslot.StartTime, updatedTimeslot.EndTime,
			updatedTimeslot.TableauId, updatedTimeslot.RadioShowId, updatedTimeslot.StudioId);

		await timeslotRepository.UpdateTimeslot(newTimeslot);

		newTimeslot.RadioHosts = this.AddRadioHosts(newTimeslot.Id, updatedTimeslot.RadioHostIds);

		if (newTimeslot.RadioShowId.HasValue) {
			newTimeslot.RadioShow = await radioShowRepository.GetRadioShow(newTimeslot.RadioShowId);
		}

		if (newTimeslot.StudioId.HasValue) {
			newTimeslot.Studio = await studioRepository.GetStudio(newTimeslot.StudioId);
		}

		return true;
	}

	public async Task<bool> DeleteTimeslot(Guid id) {
		if (await this.GetTimeslot(id) == null) {
			return false;
		}

		await timeslotRepository.DeleteTimeslot(id);
		return true;
	}

	public async Task<bool> AddHostToTimeslot(Guid id, Guid hostId) {
		Timeslot? timeslot = await this.GetTimeslot(id);
		RadioHost? radioHost = await radioHostRepository.GetHost(hostId);
		if (timeslot == null || radioHost == null ||
		    timeslot.RadioHosts.FirstOrDefault(host => host.Id == radioHost.Id) != null) {
			return false;
		}

		await timeslotRepository.CreateHostTimeslotConnection(id, hostId);
		return true;
	}

	public async Task<bool> RemoveHostFromTimeslot(Guid id, Guid hostId) {
		Timeslot? timeslot = await this.GetTimeslot(id);
		RadioHost? radioHost = await radioHostRepository.GetHost(hostId);
		if (timeslot == null || radioHost == null ||
		    timeslot.RadioHosts.FirstOrDefault(host => host.Id == radioHost.Id) == null) {
			return false;
		}

		await timeslotRepository.DeleteHostTimeslotConnection(id, hostId);
		return true;
	}

	private List<RadioHost> AddRadioHosts(Guid timeslotId, ICollection<Guid> radioHostIds) {
		List<RadioHost> newRadioHosts = [];

		radioHostIds.AsList().ForEach(hostId => {
			RadioHost? host = radioHostRepository.GetHost(hostId).Result;
			if (host == null) {
				return;
			}

			newRadioHosts.Add(host);
			timeslotRepository.CreateHostTimeslotConnection(timeslotId, host.Id);
		});

		return newRadioHosts;
	}
}
