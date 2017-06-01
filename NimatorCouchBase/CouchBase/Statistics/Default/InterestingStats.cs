using Newtonsoft.Json;

namespace NimatorCouchBase.CouchBase.Statistics.Default
{
    public class InterestingStats
    {
        [JsonProperty("cmd_get")]
        public int CmdGet { get; set; }

        [JsonProperty("couch_docs_actual_disk_size")]
        public int CouchDocsActualDiskSize { get; set; }

        [JsonProperty("couch_docs_data_size")]
        public int CouchDocsDataSize { get; set; }

        [JsonProperty("couch_spatial_data_size")]        
        public int CouchSpatialDataSize { get; set; }

        [JsonProperty("couch_spatial_disk_size")]
        public int CouchSpatialDiskSize { get; set; }

        [JsonProperty("couch_views_actual_disk_size")]
        public int CouchViewsActualDiskSize { get; set; }

        [JsonProperty("couch_views_data_size")]        
        public int CouchViewsDataSize { get; set; }

        [JsonProperty("curr_items")]
        public int CurrItems { get; set; }

        [JsonProperty("curr_items_tot")]
        public int CurrItemsTot { get; set; }

        [JsonProperty("ep_bg_fetched")]
        public int EpBgFetched { get; set; }

        [JsonProperty("get_hits")]
        public int GetHits { get; set; }

        [JsonProperty("mem_used")]
        public int MemUsed { get; set; }
        public int Ops { get; set; }

        [JsonProperty("vb_active_num_non_resident")]
        public int VbActiveNumNonResident { get; set; }

        [JsonProperty("vb_replica_curr_items")]
        public int VbReplicaCurrItems { get; set; }
    }
}