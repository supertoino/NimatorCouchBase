using System.CodeDom;
using System.Collections.Generic;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidations
    {
        IList<ILRuntimeObjectValidation> Validations();
        void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation);
    }
}