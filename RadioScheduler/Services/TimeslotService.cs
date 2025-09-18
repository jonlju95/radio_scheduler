using RadioScheduler.Interfaces;
using RadioScheduler.Models;
using RadioScheduler.Models.Api;

namespace RadioScheduler.Services;

public class TimeslotService(ITimeslotRepository timeslotRepository) {

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
		return newTimeslot;
	}

	public async Task<bool> UpdateTimeslot(Guid id, Timeslot updatedTimeslot) {
		if (await this.GetTimeslot(id) == null) {
			return false;
		}

		Timeslot newTimeslot = new Timeslot(updatedTimeslot.Id, updatedTimeslot.StartTime, updatedTimeslot.EndTime,
			updatedTimeslot.TableauId, updatedTimeslot.RadioHosts, updatedTimeslot.RadioShow, updatedTimeslot.Studio);

		await timeslotRepository.UpdateTimeslot(newTimeslot);
		return true;
	}

	public async Task<bool> DeleteTimeslot(Guid id) {
		if (await this.GetTimeslot(id) == null) {
			return false;
		}

		await timeslotRepository.DeleteTimeslot(id);
		return true;
	}
}
