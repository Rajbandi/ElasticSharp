using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;
using System.Linq;
using Org.BouncyCastle.Crypto.Digests;

namespace ElasticSharp.Core
{
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

        public static byte[] GetPublicKey (string secretPhrase)
        {
           
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            var secretBytes = Encoding.UTF8.GetBytes(secretPhrase);
            var hash = GetSha256(secretBytes);
            Algos.Curve25519.keygen(publicKey, privateKey, hash);
            return publicKey;
        }
        public static byte[] GetPublicKey(byte[] bytes)
        {
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, bytes);
            return publicKey;
        }

        public static byte[] GetPrivateKey(string secretPhrase)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretPhrase);
            var hash = GetSha256(secretBytes);
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, hash);
            return privateKey;
        }

        public static byte[] GetPrivateKey(byte[] bytes)
        {
            var hash = GetSha256(bytes);
            //var privateKey = curve25519.generatePrivateKey(hash);
            var privateKey = new byte[32];
            var publicKey = new byte[32];
            Algos.Curve25519.keygen(publicKey, privateKey, bytes);
            return privateKey;
        }


        public static string ToHex(byte[] buffer)
        {
            return BitConverter.ToString(buffer);
        }
       
        public static string ToBase64(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }
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

    }
}
