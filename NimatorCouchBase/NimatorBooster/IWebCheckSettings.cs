using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IWebCheckSettings
    {
        IHttpCallerParameters Parameters { get; }
    }
}