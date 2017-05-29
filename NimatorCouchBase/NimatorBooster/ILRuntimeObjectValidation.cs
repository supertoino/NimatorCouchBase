using Nimator;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidation
    {
        NotificationLevel NotificationLevel { get; }
        string LValidation { get; }
    }
}