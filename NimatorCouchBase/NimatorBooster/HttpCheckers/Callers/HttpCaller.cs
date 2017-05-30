using System;
using System.Net;
using Newtonsoft.Json;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
using NimatorCouchBase.NimatorBooster.Utils;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public class HttpCaller : IHttpCaller
    {
        public HttpCaller(IHttpCallerParameters pParameters)
        {
            Parameters = pParameters;
        }

        public IHttpCallerParameters Parameters { get; }

        public IRestResponse DoHttpGetCall()
        {
            return DoHttpCall(Parameters.HttpUrl, Parameters.Method, Parameters.Authenticator);
        }

        public T DoHttpGetCall<T>()
        {
            var response = DoHttpCall(Parameters.HttpUrl, Parameters.Method, Parameters.Authenticator);
            var json = DeserializeObject<T>(response.Content);
            return json;
        }

        private static T DeserializeObject<T>(string pJsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(pJsonString);
            }
            catch (Exception e)
            {
                throw new JsonException($"Could not Deserialize Json '{pJsonString}': {e.GetAllExceptionMessages()}");
            }
        }

        private static IRestResponse DoHttpCall(string pUrl, Method pRestMethod, IAuthenticator pHttpBasicAuthenticator)
        {
            try
            {
                var restClient = new RestClient(pUrl)
                {
                    Authenticator = pHttpBasicAuthenticator
                };

                var request = new RestRequest(pRestMethod)
                {
                    RequestFormat = DataFormat.Json
                };

                var response = restClient.Execute(request);
                return response;
            }
            catch (Exception e)
            {
                throw new WebException(e.GetAllExceptionMessages());
            }
        }
    }
}