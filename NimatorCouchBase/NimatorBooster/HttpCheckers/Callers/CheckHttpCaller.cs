using Newtonsoft.Json;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic.Interfaces;
using RestSharp;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public class CheckHttpCaller<T> where T : new()
    {
        public CheckHttpCaller(IHttpCaller pHttpCaller, CheckHttpCallerParameters pHttpParameters)
        {
            this.HttpCaller = pHttpCaller;
            HttpParameters = pHttpParameters;
        }

        public IHttpCaller HttpCaller { get; set; }
        public CheckHttpCallerParameters HttpParameters { get; }

        public T Call()
        {
            var result = HttpCaller.DoHttpGetCall(HttpParameters);
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