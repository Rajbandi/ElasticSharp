namespace ElasticSharp.Core.Transactions
{
    public class DataExtend : Transaction
    {
        public DataExtend() : base(TransactionType.DataExtend)
        {
        }

        public override string Name => "TaggedDataExtend";
    }
}
