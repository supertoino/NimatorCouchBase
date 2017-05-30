using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces
{
    public interface IHttpCallerParameters
    {
        string HttpUrl { get; }
        HttpAuthenticationSettings Authenticator { get; }

        HttpMethods Method { get; }
    }
}