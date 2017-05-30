using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nimator;
using Nimator.Settings;
using NimatorCouchBase.CouchBase.Checkers;
using NimatorCouchBase.NimatorBooster;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using RestSharp;
using RestSharp.Authenticators;

namespace ConsoleNimatorCouchBase
{
    class Program
    {
        // This would probably be a bit higher (e.g. 60 secs or even more) and in
        // the App.config for production scenarios:
        private const int CHECK_INTERVAL_IN_SECS = 15;

        // For ease of demo this is an embedded resource, but it could also be in a
        // seperate file or whatever persistence you'd prefer. It might be good not
        // to persist it in a database system, since your monitoring app should pro-
        // bably have as few dependencies as possible...
        private static readonly string ConfigResource = Assembly.GetExecutingAssembly().GetName().Name + ".config.json";

        // See app.config for logging setup.
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("Nimator");

        static void Main()
        {
            //Nhe();
            //log4net.Config.XmlConfigurator.Configure(); // Alternatively: http://stackoverflow.com/a/10204514/419956

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionLogger;

            Logger.Info("Creating Nimator.");

            var nimator = CreateNimator();

            Logger.Info($"Nimator created. Starting timer for cycle every {CHECK_INTERVAL_IN_SECS} seconds.");

            using (new Timer(p => nimator.TickSafe(Logger), null, 0, CHECK_INTERVAL_IN_SECS * 1000))
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            Logger.Info("Shutting down.");
        }

        private static INimator CreateNimator()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConfigResource))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();                
                return Nimator.Nimator.FromSettings(Logger, json);
            }
        }

        private static void UnhandledExceptionLogger(object pSender, UnhandledExceptionEventArgs pEventArgs)
        {
            var exc = pEventArgs.ExceptionObject as Exception;

            if (exc != null)
            {
                Logger.Fatal("Unhandled exception occurred.", exc);
            }
            else
            {
                Logger.Fatal("Fatal problem without Excption occurred.");
                Logger.Fatal(pEventArgs.ExceptionObject);
            }
        }

        private static void Nhe()
        {            
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var jsonSettings = JsonConvert.SerializeObject(CheckCouchBaseGeneralAttributesSettings.GetExampleCheckCouchBaseGeneralAttributesSettings(), settings);
            Console.WriteLine(jsonSettings);
            var jsonObject = JsonConvert.DeserializeObject<CheckCouchBaseGeneralAttributesSettings>(jsonSettings, settings);

            Console.WriteLine($"Has {jsonObject.Validations.Validations.Count} element(s)");
            Console.WriteLine();
            Console.Write("Press any key to exist...");
            Console.ReadLine();
        }
    }
}
