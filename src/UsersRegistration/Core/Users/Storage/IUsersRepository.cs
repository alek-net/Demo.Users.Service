using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UsersRegistration
{
	public interface IUsersRepository
	{
		
		Task<IEnumerable<User>> LoadUsers(Expression<Func<User, bool>> conditions);

		Task<User> CreateUser(User user);
		Task<User> UpdateUser(User user);
		Task<bool> DeleteUser(int tenantId, int userId);

		//maybe this method can be removed
		Task<bool> CheckUserExists(int tenantId, string email);

		//for performance reasons
		Task<int> GetNumberOfUsers();
		
	}
}