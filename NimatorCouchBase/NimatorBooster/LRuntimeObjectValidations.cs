using System.Collections.Generic;
using Newtonsoft.Json;

namespace NimatorCouchBase.NimatorBooster
{
    public class LRuntimeObjectValidations : ILRuntimeObjectValidations
    {
        [JsonProperty]
        private IList<ILRuntimeObjectValidation> _Validations { get; set; }

        public LRuntimeObjectValidations()
        {
            _Validations = new List<ILRuntimeObjectValidation>();    
        }

        public IList<ILRuntimeObjectValidation> Validations()
        {
            return _Validations;
        }
        public void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation)
        {
            _Validations.Add(pObjectValidation);
        }
    }
}