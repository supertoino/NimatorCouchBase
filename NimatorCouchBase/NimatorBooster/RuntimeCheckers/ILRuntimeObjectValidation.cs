using Nimator;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface ILRuntimeObjectValidation
    {
        NotificationLevel NotificationLevel { get; }
        string LValidation { get; }
    }
}