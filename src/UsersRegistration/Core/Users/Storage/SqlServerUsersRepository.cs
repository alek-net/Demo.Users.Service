using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UsersRegistration
{
	public class SqlServerUsersRepository : IUsersRepository
	{
		private UsersDbContext _ctx;

		public SqlServerUsersRepository(UsersDbContext ctx)
		{
			_ctx = ctx;
		}
		
		public async Task<IEnumerable<User>> LoadUsers(Expression<Func<User,bool>> conditions)
		{
			return await _ctx.Users.Where(conditions).ToListAsync();
		}

		public async Task<User> CreateUser(User user)
		{						
			_ctx.Users.Add(user);

			await _ctx.SaveChangesAsync();
			
			return user;
		}

		public async Task<bool> CheckUserExists(int tenantId, string email)
		{
			var existingUsers = await LoadUsers(user => user.TenantId == tenantId && user.Email == email);

			return existingUsers.Any();
		}

		public async Task<bool> DeleteUser(int tenantId, int userId)
		{
			var userToBeDeleted = await _ctx.Users
							.AsNoTracking()
							.FirstOrDefaultAsync(user => user.TenantId == tenantId && user.UserId == userId);

			if (userToBeDeleted == null)
			{
				return false;
			}

			try
			{
				_ctx.Users.Remove(userToBeDeleted);
				await _ctx.SaveChangesAsync();
				return true;
			}
			catch (DbUpdateException ex)
			{
				//Log the error
				return false;
			}			
		}

		public async Task<User> UpdateUser(User user)
		{
			_ctx.Users.Attach(user);

			await _ctx.SaveChangesAsync();

			return user;
		}

		public Task<int> GetNumberOfUsers()
		{
			throw new NotImplementedException();
		}
	}
}
