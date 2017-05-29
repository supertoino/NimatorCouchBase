using RestSharp;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic.Interfaces
{
    public interface IHttpCaller
    {
        IRestResponse DoHttpGetCall(CheckHttpCallerParameters pCallerParameters);
    }
}