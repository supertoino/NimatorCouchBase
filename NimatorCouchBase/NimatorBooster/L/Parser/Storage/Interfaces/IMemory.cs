using System.Collections.Generic;
using System.Text;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces
{
    public interface IMemory
    {
        void AddToMemory<T>(T pObject) where T : IMemoryReady;
        IMemorySlot GetFromMemory(IMemorySlotKey pMemoryKey);

        IList<IMemorySlot> GetListFromMemory(IMemorySlotKey pMemorySlotKey);
         
        void DumpMemory(StringBuilder pBuilder);
    }
}
