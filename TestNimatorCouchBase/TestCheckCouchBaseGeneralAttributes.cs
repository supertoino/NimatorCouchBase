using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nimator;
using NimatorCouchBase.CouchBase.Checkers;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.Interfaces;
using NimatorCouchBase.NimatorBooster.L;
using NimatorCouchBase.NimatorBooster.RuntimeCheckers;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestCheckCouchBaseGeneralAttributes
    {
        [TestMethod]
        public void TestCheckCouchBaseGeneralAttributesPercetangeRamAvailable()
        {
            var runExample = CreateSettingsForPercentageRamAvailable();
            IHttpCallerParameters httpCallerParameters = runExample.Parameters;
            LValidator lValidator = new LValidator();
            var checkCouchBaseRamAvailable = new CheckCouchBaseGeneralAttributes(runExample.CheckerName, lValidator, runExample.Validations, new HttpCaller(httpCallerParameters));
            var result = checkCouchBaseRamAvailable.RunAsync();
            IRuntimeObjectCheckResult runtimeObjectCheckResult = (IRuntimeObjectCheckResult)result.Result;
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
            Assert.AreEqual(NotificationLevel.Critical, runtimeObjectCheckResult.Level);
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

        [TestMethod]
        public void TestCheckCouchBaseGeneralAttributesTotalDocumentsAvailable()
        {
            var runExample = CreateSettingsForTotalDocuments();
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
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning, "StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.00000001"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error, "StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.3"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical, "StorageTotals.Hdd.UsedByData/StorageTotals.Hdd.Total>=0.5"));
            var runExample = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
            return runExample;
        }


        private static CheckCouchBaseGeneralAttributesSettings CreateSettingsForTotalDocuments()
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default",
                new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Warning, "Nodes.InterestingStats.CurrItems>=1"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Error, "Nodes.InterestingStats.CurrItems>=5"));
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical, "Nodes.InterestingStats.CurrItems>=10"));
            var runExample = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);
            return runExample;
        }
    }
}
