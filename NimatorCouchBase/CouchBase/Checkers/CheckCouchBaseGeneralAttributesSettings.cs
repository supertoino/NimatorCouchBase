// 
// NimatorCouchBase - NimatorCouchBase - CheckCouchBaseGeneralAttributesSettings.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/29/22:24
// LAST HEADER UPDATE: 2017 /05/30/20:53
// 

#region Imports

using Newtonsoft.Json;
using Nimator;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.L;
using RestSharp;

#endregion

namespace NimatorCouchBase.CouchBase.Checkers
{
    public class CheckCouchBaseGeneralAttributesSettings : IRuntimeObjectValidatorCheckSettings, IWebCheckSettings
    {
        [JsonProperty]
        public string CheckerName { get; private set; }

        public CheckCouchBaseGeneralAttributesSettings(ILRuntimeObjectValidations pValidations,
            IHttpCallerParameters pParameters)
        {
            Parameters = pParameters;
            Validations = pValidations;
        }

        /// <summary>
        ///     When called, the settings converts itself to an <see cref="T:Nimator.ICheck" /> instance. This
        ///     effectively means each <see cref="T:Nimator.ICheckSettings" /> is a mini-composition-root that
        ///     can construct concrete dependencies for an <see cref="T:Nimator.ICheck" />.
        /// </summary>
        /// <returns />
        public ICheck ToCheck()
        {
            var httpCallerParameters = Parameters; //new HttpCallerParameters("", null, HttpMethods.GET);
            HttpCaller httpCaller = new HttpCaller(httpCallerParameters);            
            return new CheckCouchBaseGeneralAttributes(CheckerName, new LValidator(), Validations, httpCaller);
        }

        [JsonProperty]
        public ILRuntimeObjectValidations Validations { get; private set; }

        [JsonProperty]
        public IHttpCallerParameters Parameters { get; private set; }

        public static CheckCouchBaseGeneralAttributesSettings GetExampleCheckCouchBaseGeneralAttributesSettings()
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default", new HttpAuthenticationSettings("supertoino","OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.01"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.1"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.5"));            
            return new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
        }
    }
}