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
        public void TestMakeRequests()
        {
            int numberOfRequestsToMake = 5;
            List<Task<int>> responseCodeResults = Program.MakeRequests(numberOfRequestsToMake);
            Assert.Equal(numberOfRequestsToMake, responseCodeResults.Count);
        }
    }
}