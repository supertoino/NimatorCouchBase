using System.Collections.Generic;

namespace NimatorCouchBase.CouchBase.Statistics.Default
{
    public class CouchBaseDefaultStats
    {
        public StorageTotals StorageTotals { get; set; }
        public int FtsMemoryQuota { get; set; }
        public int IndexMemoryQuota { get; set; }
        public int MemoryQuota { get; set; }
        public string Name { get; set; }
        public List<object> Alerts { get; set; }
        public string AlertsSilenceUrl { get; set; }
        public List<Node> Nodes { get; set; }
        public Buckets Buckets { get; set; }
        public RemoteClusters RemoteClusters { get; set; }
        public Controllers Controllers { get; set; }
        public string RebalanceStatus { get; set; }
        public string RebalanceProgressUri { get; set; }
        public string StopRebalanceUri { get; set; }
        public string NodeStatusesUri { get; set; }
        public int MaxBucketCount { get; set; }
        public AutoCompactionSettings AutoCompactionSettings { get; set; }
        public Tasks Tasks { get; set; }
        public Counters Counters { get; set; }
        public string IndexStatusUri { get; set; }
        public string CheckPermissionsUri { get; set; }
        public string ServerGroupsUri { get; set; }
        public string ClusterName { get; set; }
    }

}
