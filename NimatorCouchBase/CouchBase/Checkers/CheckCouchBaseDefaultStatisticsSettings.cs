using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimator;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.L;
using RestSharp;
using RestSharp.Authenticators;

namespace NimatorCouchBase.CouchBase.Checkers
{
    public class CheckCouchBaseDefaultStatisticsSettings : IRuntimeObjectValidatorCheckSettings
    {

        /// <summary>
        /// When called, the settings converts itself to an <see cref="T:Nimator.ICheck"/> instance. This
        ///             effectively means each <see cref="T:Nimator.ICheckSettings"/> is a mini-composition-root that
        ///             can construct concrete dependencies for an <see cref="T:Nimator.ICheck"/>.
        /// </summary>
        /// <returns/>
        public ICheck ToCheck()
        {            
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("", new HttpBasicAuthenticator("",""), Method.GET);
            HttpCaller httpCaller = new HttpCaller(httpCallerParameters);
            return new CheckCouchBaseDefaultStatistics("", new LValidator(), Validations, httpCaller);
        }

        public ILRuntimeObjectValidations Validations { get; set; }
    }
}
