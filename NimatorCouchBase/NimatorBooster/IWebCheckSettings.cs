using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;

namespace NimatorCouchBase.NimatorBooster
{
    public interface IWebCheckSettings
    {
        IHttpCallerParameters Parameters { get; }
    }
}