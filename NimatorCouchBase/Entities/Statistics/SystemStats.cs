namespace NimatorCouchBase.Entities.Statistics
{
    public class SystemStats
    {
        public double CpuUtilizationRate { get; set; }
        public long SwapTotal { get; set; }
        public long SwapUsed { get; set; }
        public long MemTotal { get; set; }
        public long MemFree { get; set; }
    }
}