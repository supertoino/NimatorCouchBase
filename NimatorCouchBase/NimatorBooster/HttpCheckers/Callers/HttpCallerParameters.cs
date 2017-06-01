using Newtonsoft.Json;
using RestSharp;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public class HttpCallerParameters : IHttpCallerParameters
    {
        public HttpCallerParameters(string pHttpUrl, HttpAuthenticationSettings pAuthenticator, HttpMethods pMethod)
        {
            Authenticator = pAuthenticator;
            Method = pMethod;
            HttpUrl = pHttpUrl;
        }
        [JsonProperty]
        public string HttpUrl { get; private set; }
        [JsonProperty]
        public HttpAuthenticationSettings Authenticator { get; private set; }
        [JsonProperty]
        public HttpMethods Method { get; private set; }
    }
}