namespace NimatorCouchBase.Entities.Statistics.Default
{
    public class Hdd
    {
        public long Total { get; set; }
        public long QuotaTotal { get; set; }
        public long Used { get; set; }
        public int UsedByData { get; set; }
        public long Free { get; set; }
    }
}