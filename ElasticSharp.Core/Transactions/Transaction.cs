using System;
using System.IO;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ElasticSharp.Core.Transactions
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
    public abstract class Transaction : IJsonObject
    {
        public abstract string Name { get; }

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

        public TransactionType TransactionType => GetTransactionType(Type, SubType);

        private int height;

       
        /// <summary>
        /// Default constructor. 
        /// </summary>
        /// <param name="type">Transaction type</param>
        protected Transaction(TransactionType type)
        {
            Type = GetTransactionType(type);
            SubType = GetTransactionSubType(type);
        }
      
        public void FromJson(string json)
        {
            var transaction = JsonConvert.DeserializeObject<Transaction>(json);
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
        /// Parse a transaction from a given hex string
        /// </summary>
        /// <param name="str">Hex string</param>
        /// <returns>Transaction data</returns>
        public static Transaction Parse(string str)
        {
            var bytes = str.FromHex();
            return Parse(bytes);

        }

        /// <summary>
        /// Parse a transaction from bytes
        /// </summary>
        /// <param name="bytes">Byte array to parse</param>
        /// <returns>Transaction data. It return nulls if byte array not in recgonizable format</returns>
        public static Transaction Parse(byte[] bytes)
        {

            Transaction transaction = null;
            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    using (var io = new BinaryReader(ms))
                    {

                        var type = io.ReadByte();
                        var subType = io.ReadByte();

                        var sub = (byte)(subType & 0x0F);
                        transaction = GetTransaction(GetTransactionType(type, sub));

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
        /// Gets native type representation of transaction from local transaction type
        /// </summary>
        /// <param name="transactionType">Transaction type</param>
        /// <returns>byte value</returns>
        public static byte GetTransactionType(TransactionType transactionType)
        {
            byte type = 0;
            switch (transactionType)
            {
                case TransactionType.PaymentOrdinary:
                case TransactionType.PaymentRedeem:
                    type = Constants.TypePayment;
                    break;
                case TransactionType.MessageAccountInfo:
                case TransactionType.MessageArbitrary:
                case TransactionType.MessageHubAnouncement:
                case TransactionType.MessagePhasingVoteCasting:
                case TransactionType.MessagePollCreation:
                case TransactionType.MessageVoteCasting:
                    type = Constants.TypeMessage;
                    break;
                case TransactionType.AccountEffectiveBalanceLeasing:
                case TransactionType.AccountPhasingOnly:
                    type = Constants.TypeAccountControl;
                    break;
                case TransactionType.DataUpload:
                case TransactionType.DataExtend:
                    type = Constants.TypeData;
                    break;
            }

            return type;
        }

        /// <summary>
        /// Returns native subtype of a given transaction from local transaction type
        /// </summary>
        /// <param name="transactionType">Transaction type</param>
        /// <returns>sub type byte</returns>
        public static byte GetTransactionSubType(TransactionType transactionType)
        {
            byte subType = 0;
            switch (transactionType)
            {
                case TransactionType.PaymentOrdinary:
                    subType = Constants.SubTypePaymentOrdinary;
                    break;
                case TransactionType.PaymentRedeem:
                    subType = Constants.SubTypePaymentRedeem;
                    break;

                case TransactionType.MessageArbitrary:
                    subType = Constants.SubTypeMessageArbitrary;
                    break;

                case TransactionType.MessagePollCreation:
                    subType = Constants.SubTypeMessagePollCreation;
                    break;

                case TransactionType.MessageVoteCasting:
                    subType = Constants.SubTypeMessageVoteCasting;
                    break;

                case TransactionType.MessageHubAnouncement:
                    subType = Constants.SubTypeMessageHubAnouncement;
                    break;

                case TransactionType.MessageAccountInfo:
                    subType = Constants.SubTypeMessageAccountInfo;
                    break;

                case TransactionType.MessagePhasingVoteCasting:
                    subType = Constants.SubTypeMessagePhasingVoteCasting;
                    break;

                case TransactionType.AccountEffectiveBalanceLeasing:
                    subType = Constants.SubTypeAccountControlBalanceLeasing;
                    break;

                case TransactionType.AccountPhasingOnly:
                    subType = Constants.SubTypeAccountControlPhasing;
                    break;

                case TransactionType.DataUpload:
                    subType = Constants.SubTypeDataUpload;
                    break;

                case TransactionType.DataExtend:
                    subType = Constants.SubTypeDataExtend;
                    break;
            }

            return subType;
        }

        /// <summary>
        /// Get transaction type from native type and subtype
        /// </summary>
        /// <param name="type">Native type</param>
        /// <param name="subType">Native subtype</param>
        /// <returns>Transaction type</returns>
        public static TransactionType GetTransactionType(byte type, byte subType)
        {
            var transactionSubType = TransactionType.PaymentOrdinary;
                if (type == Constants.TypePayment)
                {
                    if (subType == Constants.SubTypePaymentOrdinary)
                    {
                        transactionSubType = TransactionType.PaymentOrdinary;
                    }
                    else if (subType == Constants.SubTypePaymentRedeem)
                    {
                        transactionSubType = TransactionType.PaymentRedeem;
                    }
                }
                else if (type == Constants.TypeMessage)
                {
                    if (subType == Constants.SubTypeMessageArbitrary)
                    {
                        transactionSubType = TransactionType.MessageArbitrary;
                    }
                    else if (subType == Constants.SubTypeMessagePollCreation)
                    {
                        transactionSubType = TransactionType.MessagePollCreation;
                    }
                    else if (subType == Constants.SubTypeMessageVoteCasting)
                    {
                        transactionSubType = TransactionType.MessageVoteCasting;
                    }
                    else if (subType == Constants.SubTypeMessageHubAnouncement)
                    {
                        transactionSubType = TransactionType.MessageHubAnouncement;
                    }
                    else if (subType == Constants.SubTypeMessageAccountInfo)
                    {
                        transactionSubType = TransactionType.MessageAccountInfo;
                    }
                    else if (subType == Constants.SubTypeMessagePhasingVoteCasting)
                    {
                        transactionSubType = TransactionType.MessagePhasingVoteCasting;
                    }

                }
                else if (type == Constants.TypeAccountControl)
                {
                    if (subType == Constants.SubTypeAccountControlBalanceLeasing)
                    {
                        transactionSubType = TransactionType.AccountEffectiveBalanceLeasing;
                    }
                    else if (subType == Constants.SubTypeAccountControlPhasing)
                    {
                        transactionSubType = TransactionType.AccountPhasingOnly;
                    }
                }
                else if (type == Constants.TypeData)
                {
                    if (subType == Constants.SubTypeDataUpload)
                    {
                        transactionSubType = TransactionType.DataUpload;
                    }
                    else if (subType == Constants.SubTypeDataExtend)
                    {
                        transactionSubType = TransactionType.DataExtend;
                    }
                }

                return transactionSubType;
        }

        /// <summary>
        /// Creates a new transaction for a given transaction type
        /// </summary>
        /// <param name="type">Transaction type to create</param>
        /// <returns></returns>
        public static Transaction GetTransaction(TransactionType type)
        {
            Transaction transaction = null;
            switch (type)
            {
                case TransactionType.PaymentOrdinary:
                    transaction = new OrdinaryPayment();
                    break;
                case TransactionType.PaymentRedeem:
                    break;
                case TransactionType.MessageArbitrary:
                    break;
                case TransactionType.MessagePollCreation:
                    break;
                case TransactionType.MessageVoteCasting:
                    break;
                case TransactionType.MessagePhasingVoteCasting:
                    break;
                case TransactionType.MessageAccountInfo:
                    break;
                case TransactionType.MessageHubAnouncement:
                    break;
            }
            return transaction;
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
            return this.height > Constants.NQT_BLOCK;//|| Nxt.getBlockchain().getHeight() >= Constants.NQT_BLOCK;
        }

        /// <summary>
        /// Checks whether transaction is entitled for zero fee or not
        /// </summary>
        /// <returns></returns>
        public bool IsZeroFee()
        {
            return IsZeroFee(this);
        }

        /// <summary>
        /// Checks whether a given transaction is entitled for zero fee or not
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsZeroFee(Transaction t)
        {
            return t.TransactionType == TransactionType.PaymentRedeem;
        }

        //public abstract object ParseAttachment(byte[] data, byte transactionVersion);

        //public abstract object ParseAttachment(string json);

        //public abstract bool ValidateAttachment();

        
    }
}
