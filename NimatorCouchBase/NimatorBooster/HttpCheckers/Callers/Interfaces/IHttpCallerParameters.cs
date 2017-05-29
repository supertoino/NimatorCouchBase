using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces
{
    public interface IHttpCallerParameters
    {
        string HttpUrl { get; }
        IAuthenticator Authenticator { get; }

        RestSharp.Method Method { get; }
    }
}