namespace NimatorCouchBase.Entities.Statistics.Default
{
    public class Interval
    {
        public int FromHour { get; set; }
        public int ToHour { get; set; }
        public int FromMinute { get; set; }
        public int ToMinute { get; set; }
        public bool AbortOutside { get; set; }
    }
}