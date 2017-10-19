
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElasticSharp.Core;


namespace ElasticSharp.Core.Tests
{

    /// <summary>
    /// This class contains all the transaction related tests.
    /// </summary>
    [TestClass]
    public class TransactionTests
    {
        private Account _account;
      
        /// <summary>
        /// Initiaise with test account. It will read json data from a file and converts to account
        /// Example format: 
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
        /// Test for checking created transaction bytes against block chain unsigned transaction bytes 
        /// </summary>
        [TestMethod]
        public void TestTransactionBytes()
        {
            var unsignedTransaction =
                "0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c668000000809698000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000007a6cf5ab4fd016bf";
            var transaction = new Transaction
            {
                Version = 1,
                TimeStamp = 112541057,
                Deadline = 1440,
                Amount = 450000000000,
                Fee = 10000000,
                Flags = 0,
                BlockHeight = 0,
                BlockId = 13769421951337852026,
                TransactionFullHash = Crypto.ToHexEmpty(32),
                Signature = Crypto.ToHexEmpty(64)
            };

            var senderAddress = _account.Address;
            transaction.Sender = new Account
            {
                PublicKey = _account.PublicKey.ToUpper(),
                Id = Account.GetId(senderAddress),
                Address = senderAddress
            };
            var address = @"XEL-6W9S-LUCN-B5QX-E459J";
            transaction.Recipient = new Account
            {
                Address = address,
                Id = Account.GetId(address),
            };

            var transaction2 = Transaction.FromBytes(unsignedTransaction);

            Assert.AreEqual(transaction.ToJson(), transaction2.ToJson());

            var hex1 = transaction.ToBytes().ToHex();
            var hex2 = transaction2.ToBytes().ToHex();

            Assert.AreEqual(hex1, hex2);

        }

        /// <summary>
        /// Test transaction signature against a block chain signed transaction signature. 
        /// </summary>
        [TestMethod]
        public void TestTransactionSignature()
        {
            var signedTransaction =
                "0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c66800000080969800000000000000000000000000000000000000000000000000000000000000000000000000894e4708aec679a351cdafff6f15a0f9f0501d215bcc7e152e74650d130af70002a2c278b0b31d70f9695c6d15181aea96a0394c846096b217d61ba5d176d18700000000000000007a6cf5ab4fd016bf";
            var transaction = new Transaction
            {
                Version = 1,
                TimeStamp = 112541057,
                Deadline = 1440,
                Amount = 450000000000,
                Fee = 10000000,
                Flags = 0,
                BlockHeight = 0,
                BlockId = 13769421951337852026,
                TransactionFullHash = Crypto.ToHexEmpty(32),
                Signature = Crypto.ToHexEmpty(64)
            };

            var senderAddress = _account.Address;
            transaction.Sender = new Account
            {
                PublicKey = _account.PublicKey.ToUpper(),
                Id = Account.GetId(senderAddress),
                Address = senderAddress
            };
            var address = @"XEL-6W9S-LUCN-B5QX-E459J";
            transaction.Recipient = new Account
            {
                Address = address,
                Id = Account.GetId(address),
            };

            var transaction2 = Transaction.FromBytes(signedTransaction);

            //  Assert.AreEqual(transaction.ToJson(), transaction2.ToJson());

            var hex1 = Crypto.Sign(transaction.ToBytes(), _account.Secret).ToHex(); ;
            var hex2 = transaction2.Signature;

            Assert.AreEqual(hex1, hex2);
        }

    }
}