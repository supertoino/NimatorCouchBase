using NimatorCouchBase.NimatorBooster.L.Parser.Storage;

namespace NimatorCouchBase.NimatorBooster.L
{
    public interface ILValidator
    {
        bool ValidateLExpression(string pLValidation, IMemoryReady pObject);
    }
}