using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using mParticle.LoadGenerator;
using System.Collections.Generic;

namespace Tests
{
    class DummyWebRequestHandler : IWebRequestHandler
    {
        public Task<int> MakeRequest()
        {
            return Task.Run<int>(() => 200);
        }
    }
    public class WebRequestSchedulerTest
    {
        [Fact]
        public async void TestMakeRequests()
        {
            WebRequestScheduler webRequestScheduler = new WebRequestScheduler(new DummyWebRequestHandler());

            int numberOfRequestsToMake = 5;
            List<Task<int>> responseCodeResults = webRequestScheduler.MakeRequests(numberOfRequestsToMake);
            Assert.Equal(numberOfRequestsToMake, responseCodeResults.Count);
            foreach (var task in responseCodeResults)
            {
                Assert.Equal(200, await task);
            }
        }
        [Fact]
        public async void TestMakeThisManyRequestsPerCycle()
        {
            IWebRequestScheduler webRequestScheduler = new WebRequestScheduler(new DummyWebRequestHandler());
            
            int numberOfRequestsToMakePerSecond = 5;
            int numberOfSecondsToRun = 3;
            int expectedTotalNumberOfRequests = numberOfSecondsToRun * numberOfRequestsToMakePerSecond;
            var responseCodes = await webRequestScheduler.MakeThisManyRequestsPerCycle(numberOfRequestsToMakePerSecond, numberOfSecondsToRun);
            Assert.Equal(expectedTotalNumberOfRequests, responseCodes.Count);
            foreach (var responseCode in responseCodes)
            {
                Assert.Equal(200, await responseCode);
            }
        }
    }
}