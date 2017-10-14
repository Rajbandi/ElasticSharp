using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using ElasticSharp.Core.Algos;
using System.Runtime.Serialization;

namespace ElasticSharp.Core
{
    [DataContract]
    public class Account
    {
        public const string AddressPrefix = "XEL-";


        [DataMember(Name = "publicKey")]
        public string PublicKey { get; set; }

        [DataMember(Name = "id")]
        public ulong Id { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }

      

        public static ulong GetId(byte[] publicKey)
        {
            var hash = Crypto.GetSha256(publicKey);
            var hashHex = hash.ToHex();

            var arr = hash.Take(8);
            
            BigInteger bigInteger = new BigInteger(arr.ToArray());
            BigInteger strId = bigInteger;
            try
            {
                var longValue = (ulong)bigInteger;
            }
            catch (Exception)
            {
                strId = BigInteger.Add(bigInteger, Crypto.Two64);
            }

            return (ulong)strId;
        }

        public static ulong GetId(string address)
        {
            if (!string.IsNullOrWhiteSpace(address))
            {
                if (address.StartsWith(Account.AddressPrefix))
                {
                    address = address.Replace(Account.AddressPrefix, "");
                }
                var id = ReedSolomon.Decode(address);
                return id;
            }
            return 0;
        }

        public static string GetAddress(ulong id)
        {
            var address = ReedSolomon.Encode(id);
            return $"{Account.AddressPrefix}{address}";
        }

        public static string GetAddress(byte[] publicKey)
        {
            var id = GetId(publicKey);
            var address = GetAddress(id);
            return address;
        }

        public static Account GetAccount(byte[] publicKey)
        {
            var account = new Account()
            {
                PublicKey = publicKey.ToHex(),

            };
            account.Id = GetId(publicKey);
            account.Address = GetAddress(account.Id);
            return account;
        }
        public static Account GetAccount(ulong id)
        {
            var account = new Account()
            {
                Id = id,
                Address = GetAddress(id)
            };
            return account;
        }
    }
}
