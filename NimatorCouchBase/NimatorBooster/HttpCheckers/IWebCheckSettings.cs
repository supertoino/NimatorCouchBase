using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers
{
    public interface IWebCheckSettings
    {
        IHttpCallerParameters Parameters { get; }
    }
}