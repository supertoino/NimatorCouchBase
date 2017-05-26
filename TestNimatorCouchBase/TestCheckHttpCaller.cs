using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.Checkers;
using NimatorCouchBase.Entities.Statistics.Default;
using RestSharp.Authenticators;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestCheckHttpCaller
    {
        private CheckHttpCallerParameters DefaultHttpCallerParameters;

        [TestInitialize]
        public void Init()
        {
            DefaultHttpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/default", new HttpBasicAuthenticator("supertoino", "OcohoW*99"));
        }

        [TestMethod]
        public void TestCheckHttpCallerGetOk()
        {            
            var httpCallerParameters = DefaultHttpCallerParameters;

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);          
            Assert.AreEqual("supertoino", stats.ClusterName);                 
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidUrl()
        {
            var httpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoino", "OcohoW*99"));

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }

        [TestMethod]
        public void TestCheckHttpCallerGetNokInvalidCredentials()
        {
            var httpCallerParameters = new CheckHttpCallerParameters("http://localhost:8091/pools/defaultt_", new HttpBasicAuthenticator("supertoinoo", "OcohoW*99"));

            CheckHttpCaller<CouchBaseDefaultStats> checkHttpCaller = new CheckHttpCaller<CouchBaseDefaultStats>(httpCallerParameters);

            var stats = checkHttpCaller.Call();

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual(null, stats.ClusterName);
        }
    }
}
