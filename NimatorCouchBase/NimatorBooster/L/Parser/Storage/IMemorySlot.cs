using System;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public interface IMemorySlot
    {
        IMemorySlotKey Key { get; }
        Type ValueType { get; }
        object Value { get; }

        bool IsEmpty();
    }
}