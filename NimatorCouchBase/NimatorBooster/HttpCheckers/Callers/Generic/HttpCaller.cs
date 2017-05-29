using System;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic.Interfaces;
using NimatorCouchBase.NimatorBooster.Utils;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic
{
    public class HttpCaller : IHttpCaller
    {
        public IRestResponse DoHttpGetCall(CheckHttpCallerParameters pCallerParameters)
        {
            return DoHttpCall(pCallerParameters.HttpUrl, Method.GET, pCallerParameters.Authenticator);
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
                return new ErrorHttpResponse(e.GetAllExceptionMessages());
            }
        }
    }
}