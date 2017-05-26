namespace NimatorCouchBase.Entities.Statistics
{
    public class Ram
    {
        public long Total { get; set; }
        public long QuotaTotal { get; set; }
        public int QuotaUsed { get; set; }
        public long Used { get; set; }
        public int UsedByData { get; set; }
        public int QuotaUsedPerNode { get; set; }
        public long QuotaTotalPerNode { get; set; }
    }
}