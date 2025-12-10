using RadioScheduler.Models.Auth;

namespace RadioScheduler.Interfaces;

public interface IUserRepository {
	Task<IEnumerable<User>> GetUsers();
	Task<User?> GetUser(Guid id);
	Task<User?> GetUserByUsername(string username);
	Task CreateUser(User user);
	Task UpdateUser(User updatedUser);
	Task DeleteUser(Guid id);
}
