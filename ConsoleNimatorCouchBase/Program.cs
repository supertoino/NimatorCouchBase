using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nimator;
using NimatorCouchBase.CouchBase.Checkers;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using RestSharp;
using RestSharp.Authenticators;

namespace ConsoleNimatorCouchBase
{
    class Program
    {
        static void Main(string[] pArgs)
        {
            HttpCallerParameters httpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default", new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);
            LRuntimeObjectValidations lRuntimeObjectValidations = new LRuntimeObjectValidations();
            lRuntimeObjectValidations.AddObjectValidation(new LRuntimeObjectValidation(NotificationLevel.Critical, "2>1"));            
            var checkCouchBase = new CheckCouchBaseGeneralAttributesSettings(lRuntimeObjectValidations, httpCallerParameters);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var jsonSettings = JsonConvert.SerializeObject(checkCouchBase, settings);
            Console.WriteLine(jsonSettings);
            var jsonObject = JsonConvert.DeserializeObject<CheckCouchBaseGeneralAttributesSettings>(jsonSettings , settings);

            Console.WriteLine($"Has {jsonObject.Validations.Validations.Count} element(s)");
            Console.WriteLine();
            Console.Write("Press any key to exist...");
            Console.ReadLine();
        }       
    }
}
