namespace ElasticSharp.Core.Transactions
{
    public class AccountEffectiveBalanceLeasing : Transaction
    {
        public AccountEffectiveBalanceLeasing() : base(TransactionType.AccountEffectiveBalanceLeasing)
        {
        }

        public override string Name => "EffectiveBalanceLeasing";
    }
}
