using Newtonsoft.Json;
using NimatorCouchBase.Utils;
using RestSharp;

namespace NimatorCouchBase.Entities.Checkers
{
    public class CheckHttpCaller<T> where T : new()
    {
        public CheckHttpCaller(CheckHttpCallerParameters pHttpParameters)
        {
            HttpParameters = pHttpParameters;
        }

        public CheckHttpCallerParameters HttpParameters { get; }

        public T Call()
        {
            var result = WebRequests.DoHttpGetCall(HttpParameters);
            if (result is ErrorHttpResponse)
            {
                return new T();
            }
            var deserializeObject = DeserializeObject(result);
            return deserializeObject;
        }

        private static T DeserializeObject(IRestResponse pResult)
        {
            try
            {
                var deserializeObject = JsonConvert.DeserializeObject<T>(pResult.Content);
                return deserializeObject;
            }
            catch (JsonException e)
            {
                return new T();
            }
        }
    }
}