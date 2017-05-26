using System.Collections.Generic;

namespace NimatorCouchBase.Entities.Statistics
{
    public class Node
    {
        public SystemStats SystemStats { get; set; }
        public InterestingStats InterestingStats { get; set; }
        public string Uptime { get; set; }
        public long MemoryTotal { get; set; }
        public long MemoryFree { get; set; }
        public int McdMemoryReserved { get; set; }
        public int McdMemoryAllocated { get; set; }
        public string CouchApiBase { get; set; }
        public string CouchApiBaseHttps { get; set; }
        public string OtpCookie { get; set; }
        public string ClusterMembership { get; set; }
        public string RecoveryType { get; set; }
        public string Status { get; set; }
        public string OtpNode { get; set; }
        public bool ThisNode { get; set; }
        public string Hostname { get; set; }
        public int ClusterCompatibility { get; set; }
        public string Version { get; set; }
        public string Os { get; set; }
        public Ports Ports { get; set; }
        public List<string> Services { get; set; }
    }
}