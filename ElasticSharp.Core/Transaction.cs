/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: Transaction.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;
using System.IO;
using System.Runtime.Serialization;

using ElasticSharp.Core.Attachments;
using Newtonsoft.Json;

namespace ElasticSharp.Core
{
    public enum TransactionType 
    {
        PaymentOrdinary = 0,
        PaymentRedeem = 1,

        MessageArbitrary = 2,
        MessagePollCreation = 3,
        MessageVoteCasting = 4,
        MessageHubAnouncement = 5,
        MessageAccountInfo = 6,
        MessagePhasingVoteCasting = 7,

        AccountEffectiveBalanceLeasing = 8,
        AccountPhasingOnly = 9,

        DataUpload = 10,
        DataExtend = 11
    }


    [DataContract]
    public class Transaction : IJsonObject
    {
        [DataMember(Name = "type")]
        public byte Type { get; set; }

        [DataMember(Name = "subtype")]
        public byte SubType { get; set; }

        [DataMember(Name = "version")]
        public byte Version { get; set; }

        [DataMember(Name = "timestamp")]
        public int TimeStamp { get; set; }

        [DataMember(Name = "deadline")]
        public short Deadline { get; set; }


        public byte[] SenderPublicKeyBytes { get; set; }

        public ulong RecipientId { get; set; }

        [DataMember(Name = "amount")]
        public ulong Amount { get; set; }

        [DataMember(Name = "fee")]
        public ulong Fee { get; set; }

        [DataMember(Name = "referencedTransactionFullHash")]
        public string TransactionFullHash { get; set; }

        [DataMember(Name = "signature")]
        public string Signature { get; set; }

        [DataMember(Name = "flags")]
        public int Flags { get; set; }

        [DataMember(Name = "ecBlockHeight")]
        public int BlockHeight { get; set; }

        [DataMember(Name = "ecBlockId")]
        public ulong BlockId { get; set; }

        [DataMember(Name = "sender")]
        public Account Sender { get; set; }

        [DataMember(Name = "recipient")]
        public Account Recipient { get; set; }

        [DataMember(Name = "senderPublicKey")]
        public string SenderPublicKey { get; set; }


        private int height = Int32.MaxValue;
       
        /// <summary>
        /// Default constructor. 
        /// </summary>
        /// <param name="type">Transaction type</param>
        public Transaction()
        {
            TimeStamp = Int32.MaxValue;
            
        }
      
        public static Transaction FromJson(string json)
        {
            var transaction = JsonConvert.DeserializeObject<Transaction>(json);
            return transaction;
        }

        /// <summary>
        /// Gets json equivalent of current transaction
        /// </summary>
        /// <returns>Json string</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// FromBytes a transaction from a given hex string
        /// </summary>
        /// <param name="str">Hex string</param>
        /// <returns>Transaction data</returns>
        public static Transaction FromBytes(string str)
        {
            var bytes = str.FromHex();
            return FromBytes(bytes);

        }

        /// <summary>
        /// FromBytes a transaction from bytes
        /// </summary>
        /// <param name="bytes">Byte array to parse</param>
        /// <returns>Transaction data. It return nulls if byte array not in recgonizable format</returns>
        public static Transaction FromBytes(byte[] bytes)
        {

            Transaction transaction = null;
            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    using (var io = new BinaryReader(ms))
                    {
                        transaction = new Transaction();

                        var type = io.ReadByte();
                        var subType = io.ReadByte();

                        var sub = (byte)(subType & 0x0F);

                        transaction.Type = type;
                        transaction.SubType = sub;

                        var version = (byte) ((subType & 0xF0) >> 4);

                        transaction.Version = version;
                        //transaction.SubType = (byte) (subType & 0x0F);
                        transaction.TimeStamp = io.ReadInt32();
                        transaction.Deadline = io.ReadInt16();
                        var senderPublicKey = io.ReadBytes(32);

                        var recipientId = io.ReadUInt64();
                        transaction.Amount = io.ReadUInt64();
                        transaction.Fee = io.ReadUInt64();
                        transaction.TransactionFullHash = io.ReadBytes(32).ToHex();
                        transaction.Signature = io.ReadBytes(64).ToHex();
                        if (version > 0)
                        {
                            transaction.Flags = io.ReadInt32();
                            transaction.BlockHeight = io.ReadInt32();
                            transaction.BlockId = io.ReadUInt64();
                        }

                      

                        var key = senderPublicKey.ToHex();
                        if (senderPublicKey != null)
                            transaction.Sender = Account.GetAccount(senderPublicKey);


                        transaction.Recipient = Account.GetAccount(recipientId);
                    }
                }

            }
            catch (Exception ex)
            {
                transaction = null;
            }

            return transaction;
        }

        /// <summary>
        /// Get bytes from given transaction data.
        /// </summary>
        /// <param name="transaction">Transaction data</param>
        /// <param name="signature">flag indicating whether to include signature bytes or not. Default is true</param>
        /// <returns>Transaction bytes</returns>
        public static byte[] ToBytes(Transaction transaction, bool signature = true)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var io = new BinaryWriter(ms))
                    {

                        io.Write((byte) transaction.Type);

                        io.Write((byte) ((transaction.Version << 4) | transaction.SubType));

                        io.Write(transaction.TimeStamp);
                        io.Write(transaction.Deadline);
                        transaction.SenderPublicKeyBytes = transaction.Sender.PublicKey.FromHex();
                        io.Write(transaction.SenderPublicKeyBytes);
                        transaction.RecipientId = transaction.Recipient.Id;
                        io.Write(transaction.RecipientId);
                        if (transaction.UseNQT())
                        {
                            io.Write(transaction.Amount);
                            io.Write(transaction.Fee);

                            if (!string.IsNullOrWhiteSpace(transaction.TransactionFullHash))
                                io.Write(transaction.TransactionFullHash.FromHex());
                            else
                                io.Write(new byte[32]);

                        }
                        else
                        {
                            io.Write((int) (transaction.Amount / Constants.OneNxt));
                            io.Write((int) (transaction.Fee / Constants.OneNxt));
                            if (!string.IsNullOrWhiteSpace(transaction.TransactionFullHash))
                                io.Write(Crypto.GetId(transaction.TransactionFullHash.FromHex()));
                            else
                                io.Write((ulong) 0);
                        }

                        if (!string.IsNullOrWhiteSpace(transaction.Signature) && signature)
                            io.Write(transaction.Signature.FromHex());
                        else
                            io.Write(new byte[64]);

                        if (transaction.Version > 0)
                        {
                            io.Write(transaction.GetFlags());
                            io.Write(transaction.BlockHeight);
                            io.Write(transaction.BlockId);
                        }
                       
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get transaction data bytes
        /// </summary>
        /// <param name="signature">Whether to include signature or not</param>
        /// <returns></returns>
        public byte[] ToBytes(bool signature = true)
        {
            return ToBytes(this, signature);
        }
      

        /// <summary>
        /// Get transaction flags
        /// </summary>
        /// <returns></returns>
        public int GetFlags()
        {
            int flags = 0;
            int position = 1;

            return flags;
        }

        public int Size
        {
            get { return 0; }
        }

        public int SignatureOffset => 1 + 1 + 4 + 2 + 32 + 8 + (UseNQT() ? 8 + 8 + 32 : 4 + 4 + 8);

        public bool UseNQT()
        {
            return this.height > Constants.NqtBlock;//|| Nxt.getBlockchain().getHeight() >= Constants.NQT_BLOCK;
        }


    }
}
