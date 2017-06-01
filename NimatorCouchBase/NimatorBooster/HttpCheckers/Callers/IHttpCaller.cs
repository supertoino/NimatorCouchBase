using RestSharp;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public interface IHttpCaller
    {
        IHttpCallerParameters Parameters { get; }
        IRestResponse DoHttpGetCall();
        T DoHttpGetCall<T>();
    }
}