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

		Timeslot newTimeslot = new Timeslot(timeslot.Id, timeslot.StartTime, timeslot.EndTime, timeslot.TableauId,
			timeslot.RadioHosts, timeslot.RadioShow, timeslot.Studio);

		await timeslotRepository.CreateTimeslot(newTimeslot);

		newTimeslot.RadioHosts = this.AddRadioHosts(newTimeslot.Id, newTimeslot.RadioHosts);

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
			updatedTimeslot.TableauId, updatedTimeslot.RadioHosts, updatedTimeslot.RadioShow, updatedTimeslot.Studio);

		await timeslotRepository.UpdateTimeslot(newTimeslot);

		newTimeslot.RadioHosts = this.AddRadioHosts(newTimeslot.Id, newTimeslot.RadioHosts);

		if (newTimeslot.RadioShow != null) {
			newTimeslot.RadioShow = await radioShowRepository.GetRadioShow(newTimeslot.RadioShow.Id);
		}

		if (newTimeslot.Studio != null) {
			newTimeslot.Studio = await studioRepository.GetStudio(newTimeslot.Studio.Id);
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

	private List<RadioHost> AddRadioHosts(Guid timeslotId, List<RadioHost> radioHosts) {
		List<RadioHost> newRadioHosts = [];

		radioHosts.ForEach(radioHost => {
			RadioHost? host = radioHostRepository.GetHost(radioHost.Id).Result;
			if (host == null) {
				return;
			}

			newRadioHosts.Add(host);
			timeslotRepository.CreateHostTimeslotConnection(timeslotId, host.Id);
		});

		return newRadioHosts;
	}
}
