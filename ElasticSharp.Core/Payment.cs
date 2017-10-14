using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public abstract class Payment : TransactionType
    {
        public override byte Type => TYPE_PAYMENT;

       
        public override void ApplyAttachment(Transaction transaction, Account senderAccount, Account recipientAccount)
        {
           
        }

        public override bool ApplyAttachmentUnconfirmed(Transaction transaction, Account senderAccount)
        {
            return true;
        }

        public override void ValidateAttachment(Transaction transaction)
        {
           
        }
    }
}
