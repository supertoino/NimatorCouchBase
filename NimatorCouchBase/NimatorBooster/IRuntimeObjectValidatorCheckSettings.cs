using Nimator;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        ILRuntimeObjectValidations Validations { get; }
    }

    public interface IWebCheckSettings
    {
        IHttpCallerParameters Parameters { get; }
    }


}