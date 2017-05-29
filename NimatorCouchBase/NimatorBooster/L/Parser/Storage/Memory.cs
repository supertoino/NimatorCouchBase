using System.Collections.Generic;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public class Memory : IMemory
    {
        private readonly Dictionary<IMemorySlotKey, IMemorySlot> MemoryData;

        public Memory()
        {
            MemoryData = new Dictionary<IMemorySlotKey, IMemorySlot>(new MemorySlotKeyComparer());
        }      
        
        public void AddToMemory<T>(T pObject) where T : IMemoryReady
        {
            if (pObject == null) { return; }
            var memorySlots = pObject.AvailableInMemoery();
            if (memorySlots == null)
            {
                return;
            }
            foreach (var memorySlot in memorySlots)
            {
                MemoryData.Add(memorySlot.Key, memorySlot);
            }
        }

        public IMemorySlot GetFromMemory(IMemorySlotKey pMemoryKey)
        {
            IMemorySlot value;
            MemoryData.TryGetValue(pMemoryKey, out value);            
            return value ?? new MemorySlotEmpty();
        }
    }
}