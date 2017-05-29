namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces
{
    public interface IMemory
    {
        void AddToMemory<T>(T pObject) where T : IMemoryReady;
        IMemorySlot GetFromMemory(IMemorySlotKey pMemoryKey);
    }
}
