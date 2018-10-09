using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{    
	[TestClass]
	public class CryptoServiceTests
	{
		[TestMethod]
		public void HashGenerationBasicTests()
		{
			var passwordA = "tested123*";
			var passwordB = "change123*";

			var hashResultA = CryptoService.GenerateHash(passwordA);
			var hashResultB = CryptoService.GenerateHash(passwordB);
			var hashResultC = CryptoService.GenerateHash(passwordA);

			Assert.IsNotNull(hashResultA,
				"Hash result must not be null");

			Assert.IsNotNull(hashResultB,
				"Hash result must not be null");

			Assert.IsFalse(hashResultA.Hash.Equals(hashResultC.Hash),
				"Hash should be different in subsequent run for the same input");

			Assert.IsFalse(hashResultA.Salt.Equals(hashResultC.Salt),
				"Salt should be different in subsequent run for the same input");

			Assert.IsTrue(CryptoService.CheckPasswordAgainstHash(passwordA, hashResultA.Hash, hashResultA.Salt), 
				"Password can be checked against hash and salt");

			Assert.IsTrue(CryptoService.CheckPasswordAgainstHash(passwordB, hashResultB.Hash, hashResultB.Salt), 
				"Password can be checked against hash and salt");

			Assert.IsFalse(CryptoService.CheckPasswordAgainstHash(passwordA, hashResultB.Hash, hashResultB.Salt),
				"Password must not check against different hash and salt");

			Assert.IsFalse(CryptoService.CheckPasswordAgainstHash(passwordB, hashResultA.Hash, hashResultA.Salt), 
				"Password must not check against different hash and salt");


		}
	}	
}
