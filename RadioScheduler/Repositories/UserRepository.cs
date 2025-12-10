using System.Data;
using Microsoft.EntityFrameworkCore;
using RadioScheduler.Interfaces;
using RadioScheduler.Models.Auth;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories;

public class UserRepository(AppDbContext dbContext, IDbConnection dbConnection) : IUserRepository {
	public async Task<IEnumerable<User>> GetUsers() {
		return await dbContext.UserDb.ToListAsync();
	}

	public async Task<User?> GetUser(Guid id) {
		return await dbContext.UserDb.FindAsync(id);
	}

	public async Task<User?> GetUserByUsername(string username) {
		return await dbContext.UserDb.FirstOrDefaultAsync(u => u.Username == username);
	}

	public Task CreateUser(User user) {
		throw new NotImplementedException();
	}

	public async Task UpdateUser(User updatedUser) {
		await dbContext.UserDb
			.Where(u => u.Id.Equals(updatedUser.Id))
			.ExecuteUpdateAsync(user => user
				.SetProperty(u => u.FirstName, updatedUser.FirstName)
				.SetProperty(u => u.LastName, updatedUser.LastName)
				.SetProperty(u => u.Username, updatedUser.Username)
				.SetProperty(u => u.Password, updatedUser.Password)
				.SetProperty(u => u.Phone, updatedUser.Phone)
				.SetProperty(u => u.Email, updatedUser.Email)
				.SetProperty(u => u.Address, updatedUser.Address)
				.SetProperty(u => u.City, updatedUser.City)
				.SetProperty(u => u.ZipCode, updatedUser.ZipCode)
				);
	}

	public Task DeleteUser(Guid id) {
		throw new NotImplementedException();
	}
}
