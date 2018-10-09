using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UsersRegistration;

namespace Core.Tests
{
    [TestClass]
    public class UsersServiceTests
    {
		[TestMethod]
        public void GetUsersShouldReturnAllUsers()
        {
			var tenantId = 1;

			var users = GenerateUsers(tenantId).ToArray();

			var options = new DbContextOptionsBuilder<UsersDbContext>()
				.UseInMemoryDatabase(databaseName: "GetUsersShouldReturnAllUsers")
				.Options;

			// Insert seed data into the database using one instance of the context
			using (var ctx = new UsersDbContext(options))
			{
				for(int i = 0; i< users.Length;i++)
				{
					ctx.Users.Add(users[i]);
				}							
				
				ctx.SaveChanges();
			}

			// Use a clean instance of the context to run the test
			using (var ctx = new UsersDbContext(options))
			{
				var repo = new SqlServerUsersRepository(ctx);

				var service = new UsersService(repo);

				var result = service.GetUsers(tenantId).Result;

				Assert.AreEqual(users.Length, result.Count());
			}
			
        }

		[TestMethod]
		public void CreateUserBasicTests()
		{
			var tenantId = 1;

			var users = GenerateUsers(tenantId).ToArray();

			var options = new DbContextOptionsBuilder<UsersDbContext>()
				.UseInMemoryDatabase(databaseName: "CreateUserBasicTests")
				.Options;

			CreateUserResponse response;

			// Insert data into the database using one instance of the context
			using (var ctx = new UsersDbContext(options))
			{
				var repo = new SqlServerUsersRepository(ctx);

				var service = new UsersService(repo);

				var request = new CreateUserRequest()
				{
					TenantId = users[0].TenantId,
					Email = users[0].Email,
					Password = "passWORD",
					Address = users[0].Address,
					FirstName = users[0].FirstName,
					LastName = users[0].LastName,
					FavoriteFootballTeam = users[0].FavoriteFootballTeam,
					PersonalNumber = users[0].PersonalNumber
				};

				response = service.CreateUser(request).Result;

				Assert.IsNotNull(response,
					"Create user response must not be null");
			}

			// Use a clean instance of the context to run the test
			using (var ctx = new UsersDbContext(options))
			{
				Assert.AreEqual(ctx.Users.Count(), 1,
					"Create user should persist user to DB");
			}

		}

		private IEnumerable<User> GenerateUsers(int tenantId)
		{
			var hashResult = CryptoService.GenerateHash("passWORD");

			var userA = new User(
				tenantId: tenantId,
				email: "alek.net@gmail.com",
				firstName: "Aleksandar",
				lastName: "Hocevar",
				address: "Jane Sandanski 104-6",
				personalNumber: "233124123",
				favoriteFootballTeam: null,
				passwordHash: hashResult.Hash,
				passwordSalt: hashResult.Salt
			);

			yield return userA;

			hashResult = CryptoService.GenerateHash("p123!$%assWORD");

			var userB = new User(
				tenantId: tenantId,
				email: "aleksandra.net@gmail.com",
				firstName: "Aleksandra",
				lastName: "Petkovska",
				address: "Jane Sandanski 104-6",
				personalNumber: "5512123123",
				favoriteFootballTeam: null,
				passwordHash: hashResult.Hash,
				passwordSalt: hashResult.Salt
			);

			yield return userB;
		}
	}
}
