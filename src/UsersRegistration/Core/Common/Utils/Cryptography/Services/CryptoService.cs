using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
	public class CryptoService
    {		
		/// <summary>
		/// Checks a password against already generated hash and salt
		/// </summary>
		/// <param name="plainPassword"></param>
		/// <param name="hash"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static bool CheckPasswordAgainstHash(string plainPassword, string hash, string salt)
		{
			var hashResult = GenerateHash(plainPassword, salt);

			return hashResult.Hash.Equals(hash);
		}

		public static PasswordHashResult GenerateHash(string plainPassword)
		{
			var salt = GenerateRandomSalt();

			return GenerateHash(plainPassword, salt);
		}

		private static PasswordHashResult GenerateHash(string plainPassword, string salt)
		{			
			// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: plainPassword,
				salt: Convert.FromBase64String(salt),
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));
			
			var result = new PasswordHashResult(hashed, salt);

			return result;
		}

		private static string GenerateRandomSalt()
		{
			// generate a 128-bit salt using a secure PRNG
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			return Convert.ToBase64String(salt);
		}

	}
}
