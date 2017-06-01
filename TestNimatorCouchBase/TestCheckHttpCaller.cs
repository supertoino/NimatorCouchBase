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
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
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

        [TestMethod]
        public void TestCheckCouchBaseGeneralAttributesPercetangeRamAvailable()
        {
            var runExample = CreateSettingsForPercentageRamAvailable();
            IHttpCallerParameters httpCallerParameters = runExample.Parameters;
            LValidator lValidator = new LValidator();
            var checkCouchBaseRamAvailable = new CheckCouchBaseGeneralAttributes(runExample.CheckerName, lValidator, runExample.Validations, new HttpCaller(httpCallerParameters));
            var result = checkCouchBaseRamAvailable.RunAsync();
            IRuntimeObjectCheckResult runtimeObjectCheckResult = (IRuntimeObjectCheckResult) result.Result;
            Assert.IsTrue(runtimeObjectCheckResult != null);
            Assert.IsTrue(runtimeObjectCheckResult.LValidationResult);
            Assert.AreEqual(NotificationLevel.Critical, runtimeObjectCheckResult.Level);
        }

        [TestMethod]
        public void TestCheckCouchBaseGeneralAttributesRamAvailable()
        {
            var runExample = CreateSettingsForRamAvailable();
            IHttpCallerParameters httpCallerParameters = runExample.Parameters;
            LValidator lValidator = new LValidator();
            var checkCouchBaseRamAvailable = new CheckCouchBaseGeneralAttributes(runExample.CheckerName, lValidator, runExample.Validations, new HttpCaller(httpCallerParameters));
            var result = checkCouchBaseRamAvailable.RunAsync();
            IRuntimeObjectCheckResult runtimeObjectCheckResult = (IRuntimeObjectCheckResult)result.Result;
            Assert.IsTrue(runtimeObjectCheckResult != null);
            Assert.IsTrue(runtimeObjectCheckResult.LValidationResult);
            Assert.AreEqual(NotificationLevel.Critical,runtimeObjectCheckResult.Level);
        }

        [TestMethod]
        public void TestCheckCouchBaseGeneralAttributesPercetangeHddAvailable()
        {
            var runExample = CreateSettingsForPercentageHddAvailable();
            IHttpCallerParameters httpCallerParameters = runExample.Parameters;
            LValidator lValidator = new LValidator();
            var checkCouchBaseRamAvailable = new CheckCouchBaseGeneralAttributes(runExample.CheckerName, lValidator, runExample.Validations, new HttpCaller(httpCallerParameters));
            var result = checkCouchBaseRamAvailable.RunAsync();
            IRuntimeObjectCheckResult runtimeObjectCheckResult = (IRuntimeObjectCheckResult)result.Result;
            Assert.IsTrue(runtimeObjectCheckResult != null);
            Assert.IsTrue(runtimeObjectCheckResult.LValidationResult);
            Assert.AreEqual(NotificationLevel.Warning, runtimeObjectCheckResult.Level);
        }

        private static CheckCouchBaseGeneralAttributesSettings CreateSettingsForRamAvailable()
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default",
                new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning,
                "StorageTotals.Ram.Used>StorageTotals.Ram.Total*0.01"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error,
                "StorageTotals.Ram.Used>StorageTotals.Ram.Total*0.1"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical,
                "StorageTotals.Ram.Used>StorageTotals.Ram.Total*0.5"));
            var runExample = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
            return runExample;
        }

        private static CheckCouchBaseGeneralAttributesSettings CreateSettingsForPercentageRamAvailable()
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default",
                new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.01"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.1"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical, "StorageTotals.Ram.Used/StorageTotals.Ram.Total>0.5"));
            var runExample = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
            return runExample;
        }

        private static CheckCouchBaseGeneralAttributesSettings CreateSettingsForPercentageHddAvailable()
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default",
                new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning,"StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.00000001"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error,"StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.3"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical,"StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.5"));
            var runExample = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
            return runExample;
        }
    }
}
