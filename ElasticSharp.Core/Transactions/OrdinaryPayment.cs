namespace ElasticSharp.Core.Transactions
{
    public class OrdinaryPayment : Transaction
    {
        public OrdinaryPayment() : base(TransactionType.PaymentOrdinary)
        {

        }

        public override string Name => "OrdinaryPayment";




    }
}
