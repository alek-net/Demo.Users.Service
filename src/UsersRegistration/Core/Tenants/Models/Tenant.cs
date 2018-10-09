using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{
    public class Tenant
    {
		public int TenantId { get; set; }

		public string Domain { get; set; }

		public List<User> Users { get; set; }
    }
}
