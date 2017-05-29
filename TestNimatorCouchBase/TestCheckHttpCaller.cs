using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.CouchBase.Statistics.Bucker;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Generic;
using RestSharp.Authenticators;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestCheckHttpCaller
    {
        private CheckHttpCallerParameters DefaultStatsHttpCallerParameters;
        private CheckHttpCallerParameters BucketStatsHttpCallerParameters;

        [TestInitialize]
        public void TestInit()
        {
            DefaultStatsHttpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/default", new HttpBasicAuthenticator("supertoino", "OcohoW*99"));
            BucketStatsHttpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/default/buckets/supertoinoBucket/stats", new HttpBasicAuthenticator("supertoino", "OcohoW*99"));
        }

        [TestMethod]
        public void TestCheckHttpCallerGetDefaultStatsOk()
        {            
            var httpCallerParameters = DefaultStatsHttpCallerParameters;

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(new HttpCaller(), httpCallerParameters);

            var stats = checkHttpCaller.Call();
            
            Assert.AreNotEqual(stats, null); 
            Assert.AreEqual("supertoino", stats.ClusterName);                 
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidUrl()
        {
            var httpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoino", "OcohoW*99"));

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(new HttpCaller(), httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidCredentials()
        {
            var httpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoinoo", "OcohoW*99"));

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(new HttpCaller(), httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetBucketStatsOk()
        {
            var httpCallerParameters = BucketStatsHttpCallerParameters;

            CheckHttpCaller<CoachBaseBucketStats> checkHttpCaller = new CheckHttpCaller<CoachBaseBucketStats>(new HttpCaller(), httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(60, stats.Op.SamplesCount);
        }
        
    }
}
