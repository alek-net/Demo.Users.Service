namespace Core
{
	public class PasswordHashResult
	{
		public string Hash { get; protected set; }

		public string Salt { get; protected set; }

		public PasswordHashResult(string hash, string salt)
		{
			Hash = hash;
			Salt = salt;
		}
	}
}
