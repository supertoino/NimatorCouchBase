using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic.Interfaces;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers.Callers
{
    public class CheckDynamicHttpCaller : CheckHttpCaller<dynamic>
    {
        public CheckDynamicHttpCaller(IHttpCaller pHttpCaller, CheckHttpCallerParameters pHttpParameters) : base(pHttpCaller, pHttpParameters)
        {
        }
    }
}