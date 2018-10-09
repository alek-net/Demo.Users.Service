using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersRegistration;

namespace Core.Tenants.Storage
{
    public interface ITenantsRepository
    {
		Task<IEnumerable<Tenant>> LoadTenants(Expression<Func<Tenant, bool>> conditions);
	}
}
