namespace ElasticSharp.Core.Transactions
{
    public class VoteCastingMessage : Transaction
    {
        public VoteCastingMessage() : base(TransactionType.MessageVoteCasting)
        {
        }

        public override string Name => "VoteCasting";
    }
}
