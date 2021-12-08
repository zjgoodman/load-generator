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
        public Task<List<int>> MakeThisManyRequestsPerCycle(int numberOfRequestsToMakePerSecond, int numberOfSecondsToRun)
        {
            return Task.Run<List<int>>(() => {
                var responseCodes = new List<int>();
                for (int i = 0; i < numberOfSecondsToRun; i++)
                {
                    for (int request = 0; request < numberOfRequestsToMakePerSecond; request++)
                    {
                        responseCodes.Add(200);
                    }
                }
                return responseCodes;
            });
        }
        public void stop(){}
    }
    public class ProgramTest
    {
        [Fact]
        public async void TestStartRequesting()
        {
            Config config = new Config();
            config.TargetRPS = 100;
            config.NumberOfCyclesToRun = 50;
            IWebRequestScheduler webRequestScheduler = new DummyWebRequestScheduler();
            var results = await Program.StartRequesting(config, webRequestScheduler);
            int expectedNumberOfResults = (int) (config.NumberOfCyclesToRun * config.TargetRPS);
            Assert.Equal(expectedNumberOfResults, results.Count);
        }
        [Fact]
        public void TestGetResults()
        {
            List<int> responses = new List<int> {200, 200, 500};
            Results results = Program.GetResults(responses);
            Assert.Equal(2, results.SuccessCount);
            Assert.Equal(1, results.FailCount);
            Assert.Equal(3, results.TotalCount);
        }
    }
}