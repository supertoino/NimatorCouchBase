using System.Collections.Generic;

namespace NimatorCouchBase.Entities.L.Memory.Interfaces
{
    public interface IMemoryReady
    {        
        List<IMemorySlot> AvailableInMemoery();
    }
}