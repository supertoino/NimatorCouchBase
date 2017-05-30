using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimator;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
using NimatorCouchBase.NimatorBooster.L;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.CouchBase.Checkers
{
    public class CheckCouchBaseGeneralAttributesSettings : IRuntimeObjectValidatorCheckSettings, IWebCheckSettings
    {
        public CheckCouchBaseGeneralAttributesSettings(IHttpCallerParameters pParameters, ILRuntimeObjectValidations pValidations)
        {
            Parameters = pParameters;
            Validations = pValidations;
        }

        /// <summary>
        /// When called, the settings converts itself to an <see cref="T:Nimator.ICheck"/> instance. This
        ///             effectively means each <see cref="T:Nimator.ICheckSettings"/> is a mini-composition-root that
        ///             can construct concrete dependencies for an <see cref="T:Nimator.ICheck"/>.
        /// </summary>
        /// <returns/>
        public ICheck ToCheck()
        {            
            var httpCallerParameters = Parameters;
            HttpCaller httpCaller = new HttpCaller(httpCallerParameters);
            return new CheckCouchBaseGeneralAttributes("", new LValidator(), Validations, httpCaller);
        }

        public ILRuntimeObjectValidations Validations { get; }
        public IHttpCallerParameters Parameters { get; }
    }
}
