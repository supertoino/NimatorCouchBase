using Nimator;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface IRuntimeObjectCheckResult : ICheckResult
    {
        bool LValidationResult { get; }        
        string LValidation { get; }
    }
}