using Nimator;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;

namespace NimatorCouchBase.NimatorBooster.HttpCheckers
{
    public interface IWebCheck : ICheck
    {
        IHttpCaller HttpCaller { get; }        
    }
}