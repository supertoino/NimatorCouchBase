using Nimator;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        string CheckerName { get; }
        ILRuntimeObjectValidations Validations { get; }
    }
}