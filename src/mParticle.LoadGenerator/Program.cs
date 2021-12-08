using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public sealed class Program
    {
        private static volatile Boolean done = false;
        public static void Main(string[] args)
        {
            string configFile = "config.json";
            if (args.Length > 0)
            {
                configFile = args[0];
            }

            Config config = Config.GetArguments(configFile);
            if (config == null)
            {
                Console.WriteLine("Failed to parse configuration.");
                return;
            }

            Run(config);
            while (!done)
            {
                 // do nothing
            }
        }
        public static Task<List<int>> StartRequesting(Config config, IWebRequestScheduler webRequestScheduler)
        {
            int numberOfRequestsToMakePerSecond = (int) config.TargetRPS;
            int numberOfCyclesToRun = (int) config.NumberOfCyclesToRun;
            Console.WriteLine("Running " + numberOfRequestsToMakePerSecond + " requests per second until " + numberOfCyclesToRun + " seconds have elapsed...");
            return webRequestScheduler.MakeThisManyRequestsPerCycle(numberOfRequestsToMakePerSecond, numberOfCyclesToRun);
        }

        public static Results GetResults(List<int> responseCodes)
        {
            int successCount = responseCodes.FindAll( response => response == 200).Count;
            int failCount = responseCodes.Count - successCount;
            return new Results(successCount, failCount);
        }

        public static void PrintResults(Results results, int secondsElapsed)
        {
            Console.WriteLine("Time elapsed (seconds): " + secondsElapsed);
            Console.WriteLine("Total requests: " + results.TotalCount);
            Console.WriteLine("Successful requests: " + results.SuccessCount);
            Console.WriteLine("Failed requests: " + results.FailCount);
        }

        public static async void Run(Config config)
        {
            Stopwatch stopWatch = new Stopwatch();
            IWebRequestHandler webRequestHandler = new WebRequestHandler();
            IWebRequestScheduler scheduler = new WebRequestScheduler(webRequestHandler, 1000);
            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e) {
                scheduler.stop();
                e.Cancel = true;
            };
            stopWatch.Start();
            List<int> responses = await StartRequesting(config, scheduler);
            stopWatch.Stop();
            int secondsElapsed = stopWatch.Elapsed.Seconds;
            Results results = GetResults(responses);
            PrintResults(results, secondsElapsed);
            done = true;
        }
    }
}
