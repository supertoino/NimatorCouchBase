using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimator;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic;
using RestSharp.Authenticators;

namespace NimatorCouchBase.CouchBase.Checkers
{
    public class CheckCouchBaseDefaultSettings : ICheckSettings
    {
        /// <summary>
        /// When called, the settings converts itself to an <see cref="T:Nimator.ICheck"/> instance. This
        ///             effectively means each <see cref="T:Nimator.ICheckSettings"/> is a mini-composition-root that
        ///             can construct concrete dependencies for an <see cref="T:Nimator.ICheck"/>.
        /// </summary>
        /// <returns/>
        public ICheck ToCheck()
        {
            HttpCaller httpCaller = new HttpCaller();
            CheckHttpCallerParameters checkHttpCallerParameters = new CheckHttpCallerParameters("", new HttpBasicAuthenticator("",""));
            return new CheckCouchBaseDefaultStatistics("", new CheckHttpCaller<CouchBaseDefaultStats>(httpCaller, checkHttpCallerParameters ));
        }
    }
}
