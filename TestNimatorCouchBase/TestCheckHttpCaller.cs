using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.CouchBase.Statistics.Bucker;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using RestSharp;
using RestSharp.Authenticators;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestCheckHttpCaller
    {
        private HttpCallerParameters DefaultStatsHttpCallerParameters;
        private HttpCallerParameters BucketStatsHttpCallerParameters;

        [TestInitialize]
        public void TestInit()
        {
            DefaultStatsHttpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default", new HttpBasicAuthenticator("supertoino", "OcohoW*99"), Method.GET);
            BucketStatsHttpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default/buckets/supertoinoBucket/stats", new HttpBasicAuthenticator("supertoino", "OcohoW*99"), Method.GET);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetDefaultStatsOk()
        {            
            var httpCallerParameters = DefaultStatsHttpCallerParameters;

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();
            
            Assert.AreNotEqual(stats, null); 
            Assert.AreEqual("supertoino", stats.ClusterName);                 
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidUrl()
        {
            var httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoino", "OcohoW*99"), Method.GET);

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidCredentials()
        {
            var httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoinoo", "OcohoW*99"), Method.GET);

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetBucketStatsOk()
        {
            var httpCallerParameters = BucketStatsHttpCallerParameters;

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CoachBaseBucketStats>();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(60, stats.Op.SamplesCount);
        }

        [TestMethod, Ignore]
        public void TestCheckHttpCallerUsingDynamicObject()
        {
            var httpCallerParameters = BucketStatsHttpCallerParameters;

            //CheckDynamicHttpCaller checkHttpCaller = new CheckDynamicHttpCaller(new HttpCaller(httpCallerParameters), httpCallerParameters);

            //var stats = checkHttpCaller.Call();

            //Assert.AreNotEqual(stats, null);
            //Assert.AreEqual(60, stats.op.samplesCount.Value);
        }
    }
}
