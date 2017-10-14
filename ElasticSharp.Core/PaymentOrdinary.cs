using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public class PaymentOrdinary : Payment
    {
        public override byte SubType => SUBTYPE_PAYMENT_ORDINARY_PAYMENT;

    }
}
