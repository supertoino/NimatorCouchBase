using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
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