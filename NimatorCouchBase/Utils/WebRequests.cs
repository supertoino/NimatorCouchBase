using System;
using NimatorCouchBase.Entities.Checkers;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.Utils
{
    public static class WebRequests
    {
        public static IRestResponse DoHttpGetCall(CheckHttpCallerParameters pCallerParameters)
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