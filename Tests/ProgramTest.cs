using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using mParticle.LoadGenerator;
using System.Collections.Generic;

namespace Tests
{
    public class ProgramTest
    {
        [Fact]
        public async void TestMakeRequest()
        {
            Task<int> responseCode = Program.MakeRequest();
            Assert.Equal(200, await responseCode);
        }
        [Fact]
        public async void TestMakeRequests()
        {
            int numberOfRequestsToMake = 5;
            List<Task<int>> responseCodeResults = Program.MakeRequests(numberOfRequestsToMake);
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
            var responseCodes = await Program.MakeThisManyRequestsPerSecond(numberOfRequestsToMakePerSecond, numberOfSecondsToRun);
            Assert.Equal(expectedTotalNumberOfRequests, responseCodes.Count);
            foreach (var responseCode in responseCodes)
            {
                Assert.Equal(200, await responseCode);
            }
        }
    }
}