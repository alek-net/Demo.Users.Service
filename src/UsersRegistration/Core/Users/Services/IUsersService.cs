using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersRegistration
{
	public interface IUsersService
	{
		Task<CreateUserResponse> CreateUser(CreateUserRequest request);
		Task<bool> DeleteUser(DeleteUserRequest request);
		bool DoesUserExists(string email);
		Task<User> GetUserById(int userId);
		Task<IEnumerable<User>> GetUsers(int tenantId);
		Task<User> UpdateUser(UpdateUserRequest user);
		void ValidateUser(CreateUserRequest request);
	}
}