using System.Collections.Generic;

namespace NimatorCouchBase.Entities.L.Memory
{
    public interface IMemoryReady
    {        
        List<IMemorySlot> AvailableInMemoery();
    }
}