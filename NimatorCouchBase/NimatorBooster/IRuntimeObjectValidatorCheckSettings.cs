using Nimator;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        ILRuntimeObjectValidations Validations { get; }
    }
}