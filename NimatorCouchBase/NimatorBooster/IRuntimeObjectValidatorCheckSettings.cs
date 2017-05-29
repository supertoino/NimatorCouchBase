using Nimator;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IRuntimeObjectValidatorCheckSettings : ICheckSettings
    {
        ILRuntimeObjectValidations Validations { get; set; }
    }
}