using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ElasticSharp.Core
{
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

        private int height;
        public void FromJson(string json)
        {
            var transaction = JsonConvert.DeserializeObject<Transaction>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Transaction Parse(string str)
        {
            var bytes = str.FromHex();
            return Parse(bytes);

        }
        public static Transaction Parse(byte[] bytes)
        {
            var transaction = new Transaction();

            using (var ms = new MemoryStream(bytes))
            {
                using (var io = new BinaryReader(ms))
                {
                    transaction.Type = io.ReadByte();
                    transaction.SubType = io.ReadByte();
                    var version = (byte)((transaction.SubType & 0xF0) >> 4);
                    transaction.Version = version;
                    transaction.SubType = (byte)(transaction.SubType & 0x0F);
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



            return transaction;
        }

        public static byte[] ToBytes(Transaction transaction, bool signature=true)
        {
            using(var ms = new MemoryStream())
            {
                using (var io = new BinaryWriter(ms))
                {
                    io.Write((byte)transaction.Type);
                   

                    io.Write((byte)((transaction.Version << 4) | transaction.SubType));
                    
                    io.Write(transaction.TimeStamp);
                    io.Write(transaction.Deadline);
                    transaction.SenderPublicKeyBytes = transaction.Sender.PublicKey.FromHex();
                    io.Write(transaction.SenderPublicKeyBytes);
                    transaction.RecipientId = transaction.Recipient.Id;
                    io.Write(transaction.RecipientId);
                    if(transaction.UseNQT())
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
                        io.Write((int)(transaction.Amount/Constants.ONE_NXT));
                        io.Write((int)(transaction.Fee/Constants.ONE_NXT));
                        if (!string.IsNullOrWhiteSpace(transaction.TransactionFullHash))
                            io.Write(Crypto.GetId(transaction.TransactionFullHash.FromHex()));
                        else
                            io.Write((ulong)0);
                    }

                    if (!string.IsNullOrWhiteSpace(transaction.Signature) && signature)
                        io.Write(transaction.Signature.FromHex());
                    else
                        io.Write(new byte[64]);
                    
                    if(transaction.Version>0)
                    {
                        io.Write(transaction.GetFlags());
                        io.Write(transaction.BlockHeight);
                        io.Write(transaction.BlockId);
                    }
                }
                return ms.ToArray();
            }
        }

        public int GetFlags()
        {
            int flags = 0;
            int position = 1;

            return flags;
        }

        public bool UseNQT()
        {
            return this.height > Constants.NQT_BLOCK;//|| Nxt.getBlockchain().getHeight() >= Constants.NQT_BLOCK;
        }
    }
}
