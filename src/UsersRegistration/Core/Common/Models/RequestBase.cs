using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{
	public class RequestBase
    {
		[Required]
		[Range(1, int.MaxValue)]
		public int TenantId { get; set; }
	}
}
