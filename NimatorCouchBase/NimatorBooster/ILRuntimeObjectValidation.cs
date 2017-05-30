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

        public NotificationLevel NotificationLevel { get; }
        public string LValidation { get; }
    }
}