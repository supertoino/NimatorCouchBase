using System;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces
{
    public interface IMemorySlot
    {
        IMemorySlotKey Key { get; }
        Type ValueType { get; }
        object Value { get; }

        bool IsEmpty();
    }
}