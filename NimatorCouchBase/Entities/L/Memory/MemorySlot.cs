using System;

namespace NimatorCouchBase.Entities.L.Memory
{
    public class MemorySlot : IMemorySlot
    {
        public IMemorySlotKey Key { get; }
        public Type ValueType { get; }
        public object Value { get; }
        public virtual bool IsEmpty()
        {
            return false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MemorySlot(IMemorySlotKey pKey, Type pValueType, object pValue)
        {
            Key = pKey;
            ValueType = pValueType;
            Value = pValue;
        }        
    }
}