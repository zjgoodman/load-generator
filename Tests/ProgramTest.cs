using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using mParticle.LoadGenerator;
using System.Collections.Generic;

namespace Tests
{
    class DummyWebRequestScheduler : IWebRequestScheduler
    {
        public Task<List<Task<int>>> MakeThisManyRequestsPerSecond(int numberOfRequestsToMakePerSecond, int numberOfSecondsToRun)
        {
            return Task.Run<List<Task<int>>>(() => {
                var responseCodes = new List<Task<int>>();
                for (int i = 0; i < numberOfSecondsToRun; i++)
                {
                    for (int request = 0; request < numberOfRequestsToMakePerSecond; request++)
                    {
                        responseCodes.Add(Task.FromResult(200));
                    }
                    // Thread.Sleep(1000);
                }
                return responseCodes;
            });
        }
    }
    public class ProgramTest
    {
        [Fact]
        public void TestRun()
        {
            Config config = new Config();
            config.TargetRPS = 100;
            IWebRequestScheduler webRequestScheduler = new DummyWebRequestScheduler();
            Program.Run(config, webRequestScheduler);
        }
    }
}