using System.CodeDom;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidations
    {
        IList<ILRuntimeObjectValidation> Validations { get; }
        void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation);
    }

    public class LRuntimeObjectValidations : ILRuntimeObjectValidations
    {
        [JsonProperty]
        public IList<ILRuntimeObjectValidation> Validations { get; private set; }

        public LRuntimeObjectValidations()
        {
            Validations = new List<ILRuntimeObjectValidation>();    
        }        

        public void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation)
        {
            Validations.Add(pObjectValidation);
        }
    }
}