using Core.Tenants.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsersRegistration;

namespace Core.Tests.Tenants
{
	[TestClass]
    public class TenantsRepositoryTests
    {
		[TestMethod]
		public void LoadTenantsShouldReturnAllTenants()
		{
			var options = new DbContextOptionsBuilder<UsersDbContext>()
				.UseInMemoryDatabase(databaseName: "LoadTenantsShouldReturnAllTenants")
				.Options;

			// Insert seed data into the database using one instance of the context
			using (var ctx = new UsersDbContext(options))
			{
				//the seed is adding 2 tenants by default
				ctx.Database.EnsureCreated();

				ctx.Tenants.Add(new Tenant() { TenantId = 3, Domain = "asd.com" });
				
				ctx.SaveChanges();
			}

			// Use a clean instance of the context to run the test
			using (var ctx = new UsersDbContext(options))
			{
				var repo = new TenantsRepository(ctx);

				var tenants = repo.LoadTenants().Result.OrderBy(t => t.TenantId);

				Assert.AreEqual(3, tenants.Count(),
					"Not all tenants are returned");

				Assert.IsTrue(tenants.Last().Domain == "asd.com",
					"Tenant domain should be returned");
			}
		}
    }
}
