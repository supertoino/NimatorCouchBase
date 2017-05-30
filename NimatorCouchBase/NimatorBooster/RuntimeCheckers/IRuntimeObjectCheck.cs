using Nimator;
using NimatorCouchBase.NimatorBooster.L;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface IRuntimeObjectCheck<out T> : ICheck
    {
        ILValidator LValidator { get; }
        ILRuntimeObjectValidations LValidations { get; }
    }
}