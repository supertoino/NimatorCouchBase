using Newtonsoft.Json;
using Nimator;

namespace NimatorCouchBase.NimatorBooster
{
    public interface ILRuntimeObjectValidation
    {
        NotificationLevel NotificationLevel { get; }
        string LValidation { get; }
    }

    public class LRuntimeObjectValidation : ILRuntimeObjectValidation
    {
        public LRuntimeObjectValidation(NotificationLevel pNotificationLevel, string pLValidation)
        {
            NotificationLevel = pNotificationLevel;
            LValidation = pLValidation;
        }

        [JsonProperty]
        public NotificationLevel NotificationLevel { get; private set; }
        [JsonProperty]
        public string LValidation { get; private set; }
    }
}