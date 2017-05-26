namespace NimatorCouchBase.Entities.Statistics
{
    public class Ram
    {
        public long Total { get; set; }
        public long QuotaTotal { get; set; }
        public long QuotaUsed { get; set; }
        public long Used { get; set; }
        public long UsedByData { get; set; }
        public long QuotaUsedPerNode { get; set; }
        public long QuotaTotalPerNode { get; set; }
    }
}