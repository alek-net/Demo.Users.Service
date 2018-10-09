using System;
using System.Collections.Generic;
using System.Text;
using UsersRegistration;

namespace Core.Common.Services
{
    public static class ServiceFactory
    {
		public static IUsersService CreateUsersService(string connectionString)
		{
			var ctx = new UsersDbContext(connectionString);

			var usersRepository = new SqlServerUsersRepository(ctx);

			var service = new UsersService(usersRepository);

			return service;
		}
    }
}
