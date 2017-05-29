using RestSharp;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces
{
    public interface IHttpCaller
    {
        IHttpCallerParameters Parameters { get; }
        IRestResponse DoHttpGetCall();
        T DoHttpGetCall<T>();
    }
}