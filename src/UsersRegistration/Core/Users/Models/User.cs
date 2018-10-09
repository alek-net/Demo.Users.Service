using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{
    public class User
    {
		public int UserId { get; protected set; }

		[Required]
		public int TenantId { get; protected set; }

		[Required]
		[MaxLength(256)]
		public string Email { get; protected set; }

		[Required]
		[MaxLength(50)]
		public string PasswordHash { get; protected set; }

		[Required]
		[MaxLength(30)]
		public string PasswordSalt { get; protected set; }

		[Required]
		public string FirstName { get; protected set; }

		[Required]
		public string LastName { get; protected set; }

		[Required]
		public string Address { get; protected set; }

		public string PersonalNumber { get; protected set; }
		
		public string FavoriteFootballTeam { get; protected set; }
		

		protected User()
		{

		}

		public User(int tenantId, string email, string firstName, string lastName, string address, string personalNumber,
			string favoriteFootballTeam, string passwordHash, string passwordSalt)
		{
			TenantId = tenantId;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			Address = address;
			PersonalNumber = personalNumber;
			FavoriteFootballTeam = favoriteFootballTeam;
			PasswordHash = passwordHash;
			PasswordSalt = passwordSalt;
		}

		public void Update(string email, string firstName, string lastName, string address, string personalNumber,
			string favoriteFootballTeam)
		{
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			Address = address;
			PersonalNumber = personalNumber;
			FavoriteFootballTeam = favoriteFootballTeam;			
		}
	}
}
