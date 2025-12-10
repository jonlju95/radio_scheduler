using RadioScheduler.Models.Auth;

namespace RadioScheduler.Interfaces.Auth;

public interface IAuthRepository {
	Task LoginUser(UserLogin userLogin);
	Task LogoutUser();
	Task RegisterUser(User user);
}
