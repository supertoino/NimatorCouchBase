using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public interface IMemoryReady
    {        
        List<IMemorySlot> AvailableInMemoery();
    }
}