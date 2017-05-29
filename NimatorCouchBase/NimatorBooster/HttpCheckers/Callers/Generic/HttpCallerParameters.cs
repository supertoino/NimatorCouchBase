using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic
{
    public class CheckHttpCallerParameters
    {
        public CheckHttpCallerParameters(string pHttpUrl, IAuthenticator pAuthenticator)
        {
            Authenticator = pAuthenticator;
            HttpUrl = pHttpUrl;
        }

        public string HttpUrl { get; }
        public IAuthenticator  Authenticator { get; }
    }
}