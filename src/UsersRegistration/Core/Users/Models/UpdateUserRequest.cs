using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{

	public class UpdateUserRequest : RequestBase 
    {
		[Required]
		public int UserId { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Address { get; set; }

		public string PersonalNumber { get; set; }

		public string FavoriteFootballTeam { get; set; }
	}

}
