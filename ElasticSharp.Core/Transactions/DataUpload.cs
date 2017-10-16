namespace ElasticSharp.Core.Transactions
{
    public class DataUpload :Transaction
    {
        public DataUpload() : base(TransactionType.DataUpload)
        {
        }

        public override string Name => "TaggedDataUpload";
    }
}
