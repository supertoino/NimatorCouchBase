using System.Collections.Generic;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public class LMemory : IMemory
    {
        private readonly Dictionary<IMemorySlotKey, IMemorySlot> MemoryData;

        public LMemory()
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

        public void DumpMemory(StringBuilder pBuilder)
        {
            foreach (var key in MemoryData.Keys)
            {
                pBuilder.AppendLine($"{key.Key} - {MemoryData[key].ValueType} - {MemoryData[key].Value}");
            }
        }
    }
}