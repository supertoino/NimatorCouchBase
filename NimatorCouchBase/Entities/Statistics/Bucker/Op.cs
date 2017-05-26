namespace NimatorCouchBase.Entities.Statistics.Bucker
{
    public class Op
    {
        public Samples Samples { get; set; }
        public int SamplesCount { get; set; }
        public bool IsPersistent { get; set; }
        public long LastTStamp { get; set; }
        public int Interval { get; set; }
    }
}