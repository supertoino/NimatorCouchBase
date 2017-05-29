using System.Collections.Generic;

namespace NimatorCouchBase.CouchBase.Statistics.Bucker
{
    public class CoachBaseBucketStats
    {
        public Op Op { get; set; }
        public List<object> HotKeys { get; set; }
    }
}
