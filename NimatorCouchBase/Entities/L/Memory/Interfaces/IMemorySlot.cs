using System;

namespace NimatorCouchBase.Entities.L.Memory
{
    public interface IMemorySlot
    {
        IMemorySlotKey Key { get; }
        Type ValueType { get; }
        object Value { get; }

        bool IsEmpty();
    }
}