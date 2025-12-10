using System.Data;
using RadioScheduler.Interfaces.Auth;
using RadioScheduler.Models.Auth;
using RadioScheduler.Utils;

namespace RadioScheduler.Repositories.Auth;

public class AuthRepository(AppDbContext dbContext, IDbConnection dbConnection) : IAuthRepository {
	public async Task LoginUser(UserLogin userLogin) {
		dbContext.UserLoginDb.Add(userLogin);
		await dbContext.SaveChangesAsync();
	}

	public Task LogoutUser() {
		throw new NotImplementedException();
	}

	public Task RegisterUser(User user) {
		throw new NotImplementedException();
	}
}
