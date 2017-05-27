using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NimatorCouchBase.Entities.L.Memory
{
    public interface IMemory
    {
        void AddToMemory<T>(T pObject) where T : IMemoryReady;
        IMemorySlot GetFromMemory(IMemorySlotKey pMemoryKey);
    }
}
