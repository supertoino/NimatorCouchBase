using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public class HttpCallerParameters : IHttpCallerParameters
    {
        public HttpCallerParameters(string pHttpUrl, IAuthenticator pAuthenticator, Method pMethod)
        {
            Authenticator = pAuthenticator;
            Method = pMethod;
            HttpUrl = pHttpUrl;
        }

        public string HttpUrl { get; }
        public IAuthenticator  Authenticator { get; }
        public Method Method { get; }
    }
}