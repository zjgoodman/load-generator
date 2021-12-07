﻿using System;
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
        public static Task<List<Task<int>>> StartRequesting(Config config, IWebRequestScheduler webRequestScheduler)
        {
            int numberOfRequestsToMakePerSecond = (int) config.TargetRPS;
            int numberOfCyclesToRun = (int) config.NumberOfCyclesToRun;
            return webRequestScheduler.MakeThisManyRequestsPerCycle(numberOfRequestsToMakePerSecond, numberOfCyclesToRun);
        }

        public static Results GetResults(List<int> responseCodes)
        {
            int successCount = responseCodes.FindAll( response => response == 200).Count;
            int failCount = responseCodes.Count - successCount;
            return new Results(successCount, failCount);
        }
    }
}
