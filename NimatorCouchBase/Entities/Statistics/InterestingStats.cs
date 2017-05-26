namespace NimatorCouchBase.Entities.Statistics
{
    public class InterestingStats
    {
        public int CmdGet { get; set; }
        public int CouchDocsActualDiskSize { get; set; }
        public int CouchDocsDataSize { get; set; }
        public int CouchSpatialDataSize { get; set; }
        public int CouchSpatialDiskSize { get; set; }
        public int CouchViewsActualDiskSize { get; set; }
        public int CouchViewsDataSize { get; set; }
        public int CurrItems { get; set; }
        public int CurrItemsTot { get; set; }
        public int EpBgFetched { get; set; }
        public int GetHits { get; set; }
        public int MemUsed { get; set; }
        public int Ops { get; set; }
        public int VbActiveNumNonResident { get; set; }
        public int VbReplicaCurrItems { get; set; }
    }
}