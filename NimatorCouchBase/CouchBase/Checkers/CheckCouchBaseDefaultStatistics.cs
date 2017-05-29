// 
// NimatorCouchBase - NimatorCouchBase - CheckCouchBaseDefaultStatistics.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/26/18:00
// LAST HEADER UPDATE: 2017 /05/26/20:05
// 

#region Imports

using System.Threading.Tasks;
using Nimator;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;

#endregion

namespace NimatorCouchBase.CouchBase.Checkers
{
    public class CheckCouchBaseDefaultStatistics : ICheck
    {
        private readonly CheckHttpCaller<CouchBaseDefaultStats> CouchBaseDefaultStatisticsCaller;

        public CheckCouchBaseDefaultStatistics(string pShortName, CheckHttpCaller<CouchBaseDefaultStats> pCouchBaseDefaultStatisticsCaller)
        {
            ShortName = pShortName;
            CouchBaseDefaultStatisticsCaller = pCouchBaseDefaultStatisticsCaller;
        }

        /// <summary>
        ///     Every time the <see cref="T:Nimator.NimatorEngine" /> "Ticks" the check will be run.
        /// </summary>
        /// <returns>
        ///     A task representing the check calculating the current result.
        /// </returns>
        /// <remarks>
        ///     You can safely assume this method will not be called concurrently on one and the same instance
        ///     of your implementation (though it can be called concurrently on seperate different instances).
        ///     Implementations are allowed to throw exceptions, although it is encouraged to deal with the
        ///     exception types that are specific to the check (e.g. a WebException for clients that go over
        ///     a WebClient to check stuff), crafting an informative <see cref="T:Nimator.ICheckResult" /> for those
        ///     situations. If an exception flows from this method, the engine will keep running but consider
        ///     the result to be a <see cref="F:Nimator.NotificationLevel.Critical" /> failure.
        /// </remarks>
        public Task<ICheckResult> RunAsync()
        {
            var defaultStatus = CouchBaseDefaultStatisticsCaller.Call();

            CheckCouchBaseResult checkCouchBaseResult = new CheckCouchBaseResult(NotificationLevel.Okay, defaultStatus.ToString());
            return Task.FromResult<ICheckResult>(checkCouchBaseResult);
        }

        /// <summary>
        ///     A simple human-readable way to identify the Check.
        /// </summary>
        public string ShortName { get; }
    }
}