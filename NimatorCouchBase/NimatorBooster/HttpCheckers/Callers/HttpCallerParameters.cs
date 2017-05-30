using Newtonsoft.Json;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
using RestSharp;
using RestSharp.Authenticators;

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

    public enum HttpMethods
    {
        GET = 0,
        POST
    }

    public class HttpAuthenticationSettings
    {
        [JsonProperty]
        public string Username { get; private set; }
        [JsonProperty]
        public string Password { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public HttpAuthenticationSettings(string pUsername, string pPassword)
        {
            Username = pUsername;
            Password = pPassword;
        }

        public IAuthenticator ToHttpAuthenticator()
        {
            return new HttpBasicAuthenticator(Username,Password);
        }
    }
}