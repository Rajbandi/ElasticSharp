using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public abstract class TransactionType
    {
        public const byte TYPE_PAYMENT = 0;
        public const byte TYPE_MESSAGING = 1;
        public const byte TYPE_ACCOUNT_CONTROL = 2;
        public const byte TYPE_DATA = 3;

        public const byte SUBTYPE_PAYMENT_ORDINARY_PAYMENT = 0;
        public const byte SUBTYPE_PAYMENT_REDEEM = 1;

        public const byte SUBTYPE_MESSAGING_ARBITRARY_MESSAGE = 0;
        public const byte SUBTYPE_MESSAGING_POLL_CREATION = 1;
        public const byte SUBTYPE_MESSAGING_VOTE_CASTING = 2;
        public const byte SUBTYPE_MESSAGING_HUB_ANNOUNCEMENT = 3;
        public const byte SUBTYPE_MESSAGING_ACCOUNT_INFO = 4;
        public const byte SUBTYPE_MESSAGING_PHASING_VOTE_CASTING = 5;


        public const byte SUBTYPE_ACCOUNT_CONTROL_EFFECTIVE_BALANCE_LEASING = 0;
        public const byte SUBTYPE_ACCOUNT_CONTROL_PHASING_ONLY = 1;

        public const byte SUBTYPE_DATA_TAGGED_DATA_UPLOAD = 0;
        public const byte SUBTYPE_DATA_TAGGED_DATA_EXTEND = 1;

        public TransactionType() { }

        public abstract byte Type { get; }

        public abstract byte SubType { get; }

        public abstract void ValidateAttachment(Transaction transaction);

        public static bool isZeroFee(Transaction t)
        {
            if (t.Type == TYPE_PAYMENT && t.SubType == SUBTYPE_PAYMENT_REDEEM) return true;
            return false;
        }
        public bool ApplyUnconfirmed(Transaction transaction, Account account)
        {

            return true;
        }
        public void Apply(Transaction transaction, Account senderAccount, Account recipientAccount)
        {

        }

        public abstract bool ApplyAttachmentUnconfirmed(Transaction transaction, Account senderAccount);
        public abstract void ApplyAttachment(Transaction transaction, Account senderAccount, Account recipientAccount);

        public void UndoUnconfirmed(Transaction transaction, Account senderAccount)
        {

        }



    }
}
