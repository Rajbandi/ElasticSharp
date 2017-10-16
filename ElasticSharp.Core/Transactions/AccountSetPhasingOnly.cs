namespace ElasticSharp.Core.Transactions
{
    public class AccountSetPhasingOnly : Transaction
    {
        public AccountSetPhasingOnly() : base(TransactionType.AccountPhasingOnly)
        {
        }

        public override string Name => "SetPhasingOnly";
    }
}
