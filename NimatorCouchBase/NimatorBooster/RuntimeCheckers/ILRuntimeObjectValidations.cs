using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public interface ILRuntimeObjectValidations
    {
        IList<ILRuntimeObjectValidation> ValidationsOrderByNotifcationLevel();
        void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation);
    }
}