namespace NimatorCouchBase.Entities.L.Memory
{
    public class MemorySlotEmpty : MemorySlot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MemorySlotEmpty() : base(new MemorySlotKey(""), typeof(string), "") { }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}