namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public class MemorySlotKey : IMemorySlotKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MemorySlotKey(string pKey)
        {
            Key = pKey;
        }

        public string Key { get; }        
    }
}