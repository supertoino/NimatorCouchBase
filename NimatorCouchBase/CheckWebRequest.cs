using System;
using RestSharp;

namespace NimatorCouchBase
{
    public static class CheckWebRequest
    {
        public static IRestResponse DoHttpGetCall(string pUrl)
        {
            return DoHttpCall(pUrl, Method.GET);
        }

        private static IRestResponse DoHttpCall(string pUrl, Method pRestMethod)
        {
            try
            {
                var restClient = new RestClient(pUrl);

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