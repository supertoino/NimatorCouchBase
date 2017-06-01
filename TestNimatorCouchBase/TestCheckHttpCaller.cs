using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Nimator;
using NimatorCouchBase.CouchBase.Checkers;
using NimatorCouchBase.CouchBase.Statistics.Bucker;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.L;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.RuntimeCheckers;
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
            DefaultStatsHttpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default", new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            BucketStatsHttpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default/buckets/supertoinoBucket/stats", new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetDefaultStatsOk()
        {            
            var httpCallerParameters = DefaultStatsHttpCallerParameters;

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();

            LMemory memory = new LMemory();
            memory.AddToMemory(stats);
            StringBuilder sb = new StringBuilder();
            memory.DumpMemory(sb);
            Console.WriteLine(sb);

            Assert.AreNotEqual(stats, null); 
            Assert.AreEqual("supertoino", stats.ClusterName);                 
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void TestCheckHttpCallerGetNokInvalidUrl()
        {
            var httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void TestCheckHttpCallerGetNokInvalidCredentials()
        {
            var httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpAuthenticationSettings("supertoinoo", "OcohoW*99"), HttpMethods.GET);

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
