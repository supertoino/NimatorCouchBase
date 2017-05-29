using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidations
    {
        IList<ILRuntimeObjectValidation> GetObjectValidations();
        void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation);
    }
}