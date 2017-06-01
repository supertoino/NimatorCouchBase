using Nimator;
using NimatorCouchBase.NimatorBooster.L;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        string CheckerName { get; }
        ILRuntimeObjectValidations Validations { get; }
        ILValidator LValidator { get; }
    }
}