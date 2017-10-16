namespace ElasticSharp.Core.Transactions
{
    public class PhasingVoteCastingMessage : Transaction
    {
        public PhasingVoteCastingMessage() : base(TransactionType.MessagePhasingVoteCasting)
        {
        }

        public override string Name => "PhasingVoteCasting";
    }
}
