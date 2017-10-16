namespace ElasticSharp.Core.Transactions
{
    public class RedeemPayment : Transaction
    {
        public RedeemPayment() : base(TransactionType.PaymentRedeem)
        {

        }

        public override string Name => "RedeemPayment";
    }
}
