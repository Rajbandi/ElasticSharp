/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: AccountTests.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
 
***********************************************/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElasticSharp.Core.Algos;

namespace ElasticSharp.Core.Tests
{
    /// <summary>
    /// This class contains all account related test methods
    /// </summary>
    [TestClass]
    public class AccountTests
    {
        private Account _account;

        /// <summary>
        /// Initiaise with test account. It will read json data from a file and converts to account
        /// Example json: 
        /// 
        /// {"id":"", "address":"","secret":"","publickey":""}
        /// 
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var acc = System.IO.File.ReadAllText(@"c:\work\elastic\testaccount.txt");
            _account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(acc);
        }

        /// <summary>
        /// Test an account public key from a given secret
        /// </summary>
        [TestMethod]
        public void TestPublicKey()
        {
            ulong u = _account.Id;
            var publicKey = Crypto.GetPublicKey(_account.Secret);
            var id = Account.GetId(publicKey);
            Assert.AreEqual(id, u);
        }

        /// <summary>
        /// Test a public key bytes against a test account
        /// </summary>
        [TestMethod]
        public void TestPublicKeyBytes()
        {
            ulong u = _account.Id;
            var publicKey = _account.PublicKey;
            var publicKeyBytes = publicKey.FromHex();

            var id = Account.GetId(publicKeyBytes);
            Assert.AreEqual(id, u);
        }

        /// <summary>
        /// Test an id against an address
        /// </summary>
        [TestMethod]
        public void TestAccountIdFromAddress()
        {
            var address = _account.Address.Replace(Constants.AddressPrefix, "");
            var id = ReedSolomon.Decode(address);
            Assert.AreEqual(id, _account.Id);
            var addr1 = ReedSolomon.Encode(id);
            Assert.AreEqual(address, addr1);

        }
    }
}
