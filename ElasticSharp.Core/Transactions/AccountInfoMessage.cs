namespace ElasticSharp.Core.Transactions
{
    public class AccountInfoMessage : Transaction
    {
        public AccountInfoMessage() : base(TransactionType.MessageAccountInfo)
        {
        }

        public override string Name => "AccountInfo";
    }
}
