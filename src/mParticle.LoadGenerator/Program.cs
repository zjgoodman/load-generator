using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public sealed class Program
    {
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
        }
        public static async void Run(Config config, IWebRequestScheduler webRequestScheduler)
        {
            int numberOfRequestsToMakePerSecond = (int) config.TargetRPS;
            int numberOfSecondsToRun = 10;
            var results = await webRequestScheduler.MakeThisManyRequestsPerSecond(numberOfRequestsToMakePerSecond, numberOfSecondsToRun);
        }
    }
}
