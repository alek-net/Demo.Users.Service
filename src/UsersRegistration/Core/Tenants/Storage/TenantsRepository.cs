using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersRegistration;

namespace Core.Tenants.Storage
{
	public class TenantsRepository : ITenantsRepository
	{
		private UsersDbContext _ctx;

		public TenantsRepository(UsersDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<IEnumerable<Tenant>> LoadTenants()
		{
			return await _ctx.Tenants.ToListAsync();
		}

		public async Task<IEnumerable<Tenant>> LoadTenants(Expression<Func<Tenant, bool>> conditions)
		{
			return await _ctx.Tenants.Where(conditions).ToListAsync();
		}
	}
}
