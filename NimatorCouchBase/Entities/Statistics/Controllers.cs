namespace NimatorCouchBase.Entities.Statistics
{
    public class Controllers
    {
        public AddNode AddNode { get; set; }
        public Rebalance Rebalance { get; set; }
        public FailOver FailOver { get; set; }
        public StartGracefulFailover StartGracefulFailover { get; set; }
        public ReAddNode ReAddNode { get; set; }
        public ReFailOver ReFailOver { get; set; }
        public EjectNode EjectNode { get; set; }
        public SetRecoveryType SetRecoveryType { get; set; }
        public SetAutoCompaction SetAutoCompaction { get; set; }
        public ClusterLogsCollection ClusterLogsCollection { get; set; }
        public Replication Replication { get; set; }
    }
}