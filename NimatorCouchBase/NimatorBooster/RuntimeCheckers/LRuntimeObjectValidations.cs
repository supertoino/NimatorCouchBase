using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public class LRuntimeObjectValidations : ILRuntimeObjectValidations
    {
        [JsonProperty]
        private IList<ILRuntimeObjectValidation> _Validations { get; set; }

        public LRuntimeObjectValidations()
        {
            _Validations = new List<ILRuntimeObjectValidation>();    
        }

        public IList<ILRuntimeObjectValidation> ValidationsOrderByNotifcationLevel()
        {
            return _Validations.OrderByDescending(pVal => pVal.NotificationLevel).ToList();
        }
        public void AddObjectValidation(ILRuntimeObjectValidation pObjectValidation)
        {
            _Validations.Add(pObjectValidation);
        }
    }
}