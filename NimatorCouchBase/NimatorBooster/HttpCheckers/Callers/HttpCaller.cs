using System;
using System.Net;
using Newtonsoft.Json;
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
            var method = Parameters.Method == HttpMethods.GET ? Method.GET : Method.POST;
            return DoHttpCall(Parameters.HttpUrl, method, Parameters.Authenticator.ToHttpAuthenticator());
        }

        public T DoHttpGetCall<T>()
        {
            var method = Parameters.Method == HttpMethods.GET ? Method.GET : Method.POST;
            var response = DoHttpCall(Parameters.HttpUrl, method, Parameters.Authenticator.ToHttpAuthenticator());
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
                throw new JsonException($"Could not Deserialize Json '{pJsonString}': {e.Message}");
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
                throw new WebException(e.Message);
            }
        }
    }
}