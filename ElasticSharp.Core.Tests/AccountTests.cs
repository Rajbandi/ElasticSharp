using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElasticSharp.Core.Algos;

namespace ElasticSharp.Core.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestPublicKey()
        {
            ulong u = Account.Id;
            var publicKey = Crypto.GetPublicKey(Account.Secret);
            var id = Account.GetId(publicKey);
            Assert.AreEqual(id, u);
        }

        [TestMethod]
        public void TestPublicKeyBytes()
        {
            ulong u = Account.Id;
            var publicKey = Account.PublicKey;
            var publicKeyBytes = publicKey.FromHex();

            var id = Account.GetId(publicKeyBytes);
            Assert.AreEqual(id, u);
        }
        [TestMethod]
        public void TestAccountIdFromAddress()
        {
            var address = Account.Address.Replace("XEL-", "");
            var id = ReedSolomon.Decode(address);
            Assert.AreEqual(id, Account.Id);
            var addr1 = ReedSolomon.Encode(id);
            Assert.AreEqual(address, addr1);

        }

        private Account _account;
        public Account Account
        {
            get
            {
                if (_account == null)
                {
                    var acc = System.IO.File.ReadAllText(@"c:\work\elastic\testaccount.txt");
                    _account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(acc);
                }
                return _account;
            }
        }
    }
}
