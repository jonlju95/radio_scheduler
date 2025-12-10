using Microsoft.AspNetCore.Identity;
using RadioScheduler.Models.Auth;

namespace RadioScheduler.Utils.AuthHelpers;

public static class PasswordHasher {
	public static string HashPassword(User user, string password) {
		PasswordHasher<User> hasher = new PasswordHasher<User>();
		return hasher.HashPassword(user, password);
	}

	public static PasswordVerificationResult VerifyPassword(User user, string password) {
		PasswordHasher<User> hasher = new PasswordHasher<User>();
		string hashedPassword = hasher.HashPassword(user, password);
		return hasher.VerifyHashedPassword(user, hashedPassword, password);
	}
}
