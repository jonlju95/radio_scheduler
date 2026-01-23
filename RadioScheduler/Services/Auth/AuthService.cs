using Microsoft.AspNetCore.Identity;
using RadioScheduler.Interfaces;
using RadioScheduler.Interfaces.Auth;
using RadioScheduler.Models.Auth;
using RadioScheduler.Utils.AuthHelpers;

namespace RadioScheduler.Services.Auth;

public class AuthService(IAuthRepository authRepository, IUserRepository userRepository) {
	public async Task<User?> LoginUser(string username, string password) {
		User? user = await userRepository.GetUserByUsername(username);
		if (user == null) {
			return null;
		}

		if (PasswordHasher.VerifyPassword(user, password) == PasswordVerificationResult.Failed) {
			return null;
		}

		UserLogin newUserLogin = new UserLogin(user.Id);
		await authRepository.LoginUser(newUserLogin);

		return user;
	}

	public Task LogoutUser() {
		throw new NotImplementedException();
	}

	public Task RegisterUser(User user) {
		throw new NotImplementedException();
	}
}
