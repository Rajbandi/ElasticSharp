namespace ElasticSharp.Core.Transactions
{
    public class HubAnnouncementMessage : Transaction
    {
        public HubAnnouncementMessage(TransactionType type) : base(type)
        {
        }

        public override string Name => "HubAnnouncement";
    }
}
