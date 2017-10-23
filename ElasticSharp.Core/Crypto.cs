/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: Crypto.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.Text;
using System.Numerics;
using System.Linq;
using Org.BouncyCastle.Crypto.Digests;

namespace ElasticSharp.Core
{
    /// <summary>
    /// This class contains important crypto related methods.
    /// </summary>
    public class Crypto
    {
        public class KeyPair
        {
            public byte[] PublicKey { get; set; }
            public byte[] PrivateKey { get; set; }
        }
        //public static byte[] GetSha256(byte[] buffer)
        //{
        //    var algo = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);

        //    return algo.HashData(buffer);
        //}
        public static readonly BigInteger Two64 = BigInteger.Parse("18446744073709551616");

        /// <summary>
        /// Get an id from a given hash string
        /// </summary>
        /// <param name="hashStr">hash string</param>
        /// <returns></returns>
        public static ulong GetId(string hashStr)
        {
            var hash = hashStr.FromHex();
            var arr = hash.Take(8);

            BigInteger bigInteger = new BigInteger(arr.ToArray());
            BigInteger strId = bigInteger;
            try
            {
                var longValue = (ulong)bigInteger;
            }
            catch (Exception)
            {
                strId = BigInteger.Add(bigInteger, Two64);
            }

            return (ulong)strId;
        }

        /// <summary>
        /// Get an id from a given byte array
        /// </summary>
        /// <param name="hash">byte array</param>
        /// <returns></returns>
        public static ulong GetId(byte[] hash)
        {
           
            var arr = hash.Take(8);

            BigInteger bigInteger = new BigInteger(arr.ToArray());
            BigInteger strId = bigInteger;
            try
            {
                var longValue = (ulong)bigInteger;
            }
            catch (Exception)
            {
                strId = BigInteger.Add(bigInteger, Two64);
            }

            return (ulong)strId;
        }

        /// <summary>
        /// Get keys from a given secret phrase
        /// </summary>
        /// <param name="secretPhrase">Secret phrase</param>
        /// <returns></returns>
        public static KeyPair GetKeys(string secretPhrase)
        {
           
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            var secretBytes = Encoding.UTF8.GetBytes(secretPhrase);
            var hash = GetSha256(secretBytes);
            Algos.Curve25519.keygen(publicKey, privateKey, hash);
            return new KeyPair
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };
        }

        /// <summary>
        /// Get a public key from a secret phrase
        /// </summary>
        /// <param name="secretPhrase">Secret phrase</param>
        /// <returns></returns>
        public static byte[] GetPublicKey (string secretPhrase)
        {
           
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            var secretBytes = Encoding.UTF8.GetBytes(secretPhrase);
            var hash = GetSha256(secretBytes);
            Algos.Curve25519.keygen(publicKey, privateKey, hash);
            return publicKey;
        }

        /// <summary>
        /// Get a public key from a given byte array
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <returns></returns>
        public static byte[] GetPublicKey(byte[] bytes)
        {
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, bytes);
            return publicKey;
        }

        /// <summary>
        /// Get a private key from a given secret phrase.
        /// </summary>
        /// <param name="secretPhrase">Secret phrase</param>
        /// <returns></returns>
        public static byte[] GetPrivateKey(string secretPhrase)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretPhrase);
            var hash = GetSha256(secretBytes);
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, hash);
            return privateKey;
        }

        /// <summary>
        /// Get a private key from a given byte array
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns></returns>
        public static byte[] GetPrivateKey(byte[] bytes)
        {
            var hash = GetSha256(bytes);
            //var privateKey = curve25519.generatePrivateKey(hash);
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, bytes);
            return privateKey;
        }

        /// <summary>
        /// Converts bytes to hex string
        /// </summary>
        /// <param name="buffer">Bytes to convert</param>
        /// <returns></returns>
        public static string ToHex(byte[] buffer)
        {
            return BitConverter.ToString(buffer);
        }
       
        /// <summary>
        /// Converts a given byte array to a base 64 string
        /// </summary>
        /// <param name="buffer">Byte array</param>
        /// <returns></returns>
        public static string ToBase64(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Signs a message with a secret phrase
        /// </summary>
        /// <param name="message"></param>
        /// <param name="secretPhrase"></param>
        /// <returns></returns>
        public static byte[] Sign(byte[] message, string secretPhrase)
        {

            byte[] P = new byte[32];
            byte[] s = new byte[32];
            var secret = GetSha256(secretPhrase);
            Algos.Curve25519.keygen(P, s, secret);

            byte[] m = GetSha256(message);

            byte[] x = GetSha256(m, s);

            byte[] Y = new byte[32];
            Algos.Curve25519.keygen(Y, null, x);

            byte[] h = GetSha256(m, Y);

            byte[] v = new byte[32];
            Algos.Curve25519.sign(v, h, x, s);

            byte[] signature = new byte[64];
            Buffer.BlockCopy(v, 0, signature, 0, 32);
            Buffer.BlockCopy(h, 0, signature, 32, 32);
            return signature;
        }

        /// <summary>
        /// Gets sha 256 hash from a given string 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetSha256(string data)
        {
            return GetSha256(Encoding.UTF8.GetBytes(data));
        }
        //public static byte[] GetSha256(byte[] data)
        //{
        //    var sha256 = SHA256.Create();

        //    var hash = sha256.ComputeHash(data);
        //    return hash;

        //}
        /// <summary>
        /// Gets sha 256 bytes from a given byte arrays
        /// </summary>
        /// <param name="messages">byte arrays</param>
        /// <returns></returns>
        public static byte[] GetSha256(params byte[][] messages)
        {
            var digest = new Sha256Digest();
            var arrayMessages = messages.Select(x => x).ToArray();

            foreach (var byteArr in arrayMessages)
            {
                digest.BlockUpdate(byteArr, 0, byteArr.Length);
            }

            var bytes = new byte[digest.GetDigestSize()];
            digest.DoFinal(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// Converts a empty byte array to hex string
        /// </summary>
        /// <param name="length">Byte lenth</param>
        /// <returns></returns>
        public static string ToHexEmpty(int length)
        {
            return new byte[length].ToHex();
        }
    }
}
