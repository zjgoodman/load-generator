using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using mParticle.LoadGenerator;
using System.Collections.Generic;

namespace Tests
{
    public class WebRequestSchedulerTest
    {
        WebRequestScheduler webRequestScheduler = new WebRequestScheduler();
        [Fact]
        public async void TestMakeRequest()
        {
            Task<int> responseCode = webRequestScheduler.MakeRequest();
            Assert.Equal(200, await responseCode);
        }
        [Fact]
        public async void TestMakeRequests()
        {
            int numberOfRequestsToMake = 5;
            List<Task<int>> responseCodeResults = webRequestScheduler.MakeRequests(numberOfRequestsToMake);
            Assert.Equal(numberOfRequestsToMake, responseCodeResults.Count);
            foreach (var task in responseCodeResults)
            {
                Assert.Equal(200, await task);
            }
        }
        [Fact]
        public async void TestMakeThisManyRequestsPerSecond()
        {
            int numberOfRequestsToMakePerSecond = 5;
            int numberOfSecondsToRun = 3;
            int expectedTotalNumberOfRequests = numberOfSecondsToRun * numberOfRequestsToMakePerSecond;
            var responseCodes = await webRequestScheduler.MakeThisManyRequestsPerSecond(numberOfRequestsToMakePerSecond, numberOfSecondsToRun);
            Assert.Equal(expectedTotalNumberOfRequests, responseCodes.Count);
            foreach (var responseCode in responseCodes)
            {
                Assert.Equal(200, await responseCode);
            }
        }
    }
}