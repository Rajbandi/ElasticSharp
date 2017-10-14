﻿using System;
using ElasticSharp.Core;

namespace ElasticSharp.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //retrieves secret phrase from text file
            var secret = Account.Secret;

            var transactionStr = "0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c668000000809698000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000007a6cf5ab4fd016bf";

            var transactionSigned = "0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c66800000080969800000000000000000000000000000000000000000000000000000000000000000000000000894e4708aec679a351cdafff6f15a0f9f0501d215bcc7e152e74650d130af70002a2c278b0b31d70f9695c6d15181aea96a0394c846096b217d61ba5d176d18700000000000000007a6cf5ab4fd016bf";

            //"0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c668000000809698000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000007a6cf5ab4fd016bf 

            //"0010813db506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54f870429596621cc8001417c66800000080969800000000000000000000000000000000000000000000000000000000000000000000000000894e4708aec679a351cdafff6f15a0f9f0501d215bcc7e152e74650d130af70002a2c278b0b31d70f9695c6d15181aea96a0394c846096b217d61ba5d176d18700000000000000007a6cf5ab4fd016bf";

            //"0114289cb506a0059fa3a0f6bd401b40c8e9917da4cf1900a4087e8a3cba6001f6e461a32a9b3a54c49594c5ca91d96b00000000000000008096980000000000000000000000000000000000000000000000000000000000000000000000000098203bdbe240e7f1969cdbd44123b8e73274afc2e42f7df48872f5610f9e9d063ba776187fdb49c5cef466e59cf1a3a8467e83e0307242075e4f88ce9fb6a1c400000000000000007a6cf5ab4fd016bf01074465764b696e670f004465764b696e672041646472657373";
            //"001026f5ca06a005643796f05cc7a600a84919438e316e05c838ceedfeace0f95168265d57331e5a705b1455ad998c6d00581d0a8c03000080969800000000000000000000000000000000000000000000000000000000000000000000000000833c5a39d6237abb06e56a1ce268fc86d89d3a27d1a7127d773fe8010f80f003d396be6395038fe67316548982b1067620ca86fabc2bca157ed01b270ccd6ba220000000000000007a6cf5ab4fd016bf017c4508f5a48d44e4b88de7656e76ca5a653ee7f98253bdf705e0635b933525cb";
            //"0010d5d7ca06a005317cad5babf951f6d9c35f6d21a31e89dfe197c5e6c17628a2023945946623125c47126c87f5b9b30065cd1d00000000809698000000000000000000000000000000000000000000000000000000000000000000000000006a8a1da8c16dbaebf80ef975a21365e28eadd9dd981d60ec76dcf5f5376ff10bfa6731a4d8ca83d146e0186fd2231a448ca49f795cffcf42198971039ffceab300000000ce9000003ba9c74cc8503f56";
            var transactionBytes = transactionStr.FromHex();
            var signedTranBytes = transactionSigned.FromHex();
            var transaction = Transaction.Parse(transactionBytes);
            var signedTransaction = Transaction.Parse(signedTranBytes);
            var signature = transaction.Signature;

            var tranBytes = Transaction.ToBytes(transaction, false);
            Console.WriteLine(BitConverter.ToString(signedTranBytes));
            Console.WriteLine("".PadRight(100, '-'));
            Console.WriteLine(BitConverter.ToString(tranBytes));
            Console.WriteLine("".PadRight(100, '-'));

            Console.WriteLine(transaction.ToJson());
            Console.WriteLine("".PadRight(100, '-'));
            Console.WriteLine(signedTransaction.ToJson());


            var signature1 = Crypto.Sign(tranBytes, secret);

            Console.WriteLine(tranBytes.Length);

            Console.WriteLine(BitConverter.ToString(signedTransaction.Signature.FromHex()));
            Console.WriteLine(BitConverter.ToString(signature1));
        }

        private static Account Account
        {
            get
            {
                var acc = System.IO.File.ReadAllText(@"c:\work\elastic\testaccount.txt");

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(acc);
            }

        }
    }
}