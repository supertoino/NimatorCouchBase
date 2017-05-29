using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace NimatorCouchBase.NimatorBooster.L
{
    public interface ILValidator
    {
        bool ValidateLExpression(string pLValidation, IMemoryReady pObject);
    }
}