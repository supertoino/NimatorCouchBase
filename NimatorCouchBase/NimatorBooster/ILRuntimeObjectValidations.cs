using System.CodeDom;
using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidations
    {
        IList<ILRuntimeObjectValidation> ValidationsOrderByNotifcationLevel();
        void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation);
    }
}