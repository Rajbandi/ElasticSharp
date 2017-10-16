namespace ElasticSharp.Core.Transactions
{
    public class PollCreationMessage : Transaction
    {
        public PollCreationMessage() : base(TransactionType.MessagePollCreation)
        {
        }

        public override string Name => "PollCreation";
    }
}
