using Core.Tenants.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{
    public class TenantsService
    {
		private TenantsRepository _tenantsRepository;

		public TenantsService(int tenantID, TenantsRepository tenantsRepository)
		{

		}

		public async Task<Tenant> GetTenantById(int tenantId)
		{
			var tenants = await _tenantsRepository.LoadTenants(tenant => tenant.TenantId == tenantId);

			return tenants.FirstOrDefault();
		}

		public async Task<IEnumerable<Tenant>> GetTenants()
		{
			var tenants = await _tenantsRepository.LoadTenants(null);

			return tenants;
		}
	}
}
