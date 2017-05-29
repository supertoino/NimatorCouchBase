using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces
{
    public interface IMemoryReady
    {        
        List<IMemorySlot> AvailableInMemoery();
    }
}