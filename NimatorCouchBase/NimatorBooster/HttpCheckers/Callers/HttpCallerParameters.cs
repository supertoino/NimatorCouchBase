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
        
        public string HttpUrl { get; }
        public HttpAuthenticationSettings Authenticator { get; }
        public HttpMethods Method { get; }
    }

    public enum HttpMethods
    {
        GET = 0,
        POST
    }

    public class HttpAuthenticationSettings
    {
        public string Username { get; }
        public string Password { get; }

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