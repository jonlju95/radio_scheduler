using RadioScheduler.Interfaces;
using RadioScheduler.Models.Auth;
using RadioScheduler.Utils.AuthHelpers;

namespace RadioScheduler.Services;

public class UserService(IUserRepository userRepository) {

	public async Task<IEnumerable<User>> GetUsers() {
		return await userRepository.GetUsers();
	}

	public async Task<User?> GetUser(Guid id) {
		return await userRepository.GetUser(id);
	}

	public async Task<User?> GetUserByUsername(string username) {
		return await userRepository.GetUserByUsername(username);
	}

	public async Task<bool> UpdateUser(Guid id, User updatedUser) {
		if (await this.GetUser(id) == null) {
			return false;
		}

		string hashedPassword = PasswordHasher.HashPassword(updatedUser, updatedUser.Password);

		User newUser = new User(id,
			updatedUser.FirstName,
			updatedUser.LastName,
			updatedUser.Username,
			hashedPassword,
			updatedUser.Phone,
			updatedUser.Email,
			updatedUser.Address,
			updatedUser.City,
			updatedUser.ZipCode
		);

		await userRepository.UpdateUser(newUser);
		return true;
	}
}
