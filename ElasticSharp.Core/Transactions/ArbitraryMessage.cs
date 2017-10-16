namespace ElasticSharp.Core.Transactions
{
    public class ArbitraryMessage : Transaction
    {
        public ArbitraryMessage() : base(TransactionType.MessageArbitrary)
        {
        }

        public override string Name => "ArbitraryMessage";
    }
}
