namespace NimatorCouchBase.Entities.Statistics
{
    public class AutoCompactionSettings
    {
        public bool ParallelDbAndViewCompaction { get; set; }
        public DatabaseFragmentationThreshold DatabaseFragmentationThreshold { get; set; }
        public ViewFragmentationThreshold ViewFragmentationThreshold { get; set; }
        public string IndexCompactionMode { get; set; }
        public IndexCircularCompaction IndexCircularCompaction { get; set; }
        public IndexFragmentationThreshold IndexFragmentationThreshold { get; set; }
    }
}