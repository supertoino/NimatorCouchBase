using Nimator;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        string CheckerName { get; }
        ILRuntimeObjectValidations Validations { get; }
    }
}