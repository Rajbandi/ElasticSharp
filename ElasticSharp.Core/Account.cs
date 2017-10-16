using System;
using System.Numerics;
using System.Linq;
using ElasticSharp.Core.Algos;
using System.Runtime.Serialization;

namespace ElasticSharp.Core
{
    /// <summary>
    /// This class represents an address account 
    /// </summary>
    [DataContract]
    public class Account
    {

        [DataMember(Name = "publicKey")]
        public string PublicKey { get; set; }

        [DataMember(Name = "id")]
        public ulong Id { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }

      
        /// <summary>
        /// Converts a given public key to address id
        /// </summary>
        /// <param name="publicKey">Public key bytes to convert</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts a given address to id
        /// </summary>
        /// <param name="address">Address to convert</param>
        /// <returns></returns>
        public static ulong GetId(string address)
        {
            if (!string.IsNullOrWhiteSpace(address))
            {
                if (address.StartsWith(Constants.AddressPrefix))
                {
                    address = address.Replace(Constants.AddressPrefix, "");
                }
                var id = ReedSolomon.Decode(address);
                return id;
            }
            return 0;
        }

        /// <summary>
        /// Gets an address from a given id
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns></returns>
        public static string GetAddress(ulong id)
        {
            var address = ReedSolomon.Encode(id);
            return $"{Constants.AddressPrefix}{address}";
        }

        /// <summary>
        /// Gets an address from a given public key bytes
        /// </summary>
        /// <param name="publicKey">Public key bytes</param>
        /// <returns></returns>
        public static string GetAddress(byte[] publicKey)
        {
            var id = GetId(publicKey);
            var address = GetAddress(id);
            return address;
        }

        /// <summary>
        /// Gets an account from a given public key.
        /// </summary>
        /// <param name="publicKey">Public key bytes</param>
        /// <returns></returns>
        public static Account GetAccount(byte[] publicKey)
        {
            var account = new Account
            {
                PublicKey = publicKey.ToHex(),
                Id = GetId(publicKey),
            };
            account.Address = GetAddress(account.Id);
            return account;
        }

        /// <summary>
        /// Gets an account from a given address id
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns></returns>
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
