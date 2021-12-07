using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public class WebRequestScheduler : IWebRequestScheduler
    {
        private IWebRequestHandler webRequestHandler;
        private int numberOfMilliSecondsToWaitBetweenCycles;
        public WebRequestScheduler(IWebRequestHandler webRequestHandler, int numberOfMilliSecondsToWaitBetweenCycles)
        {
            this.webRequestHandler = webRequestHandler;
            this.numberOfMilliSecondsToWaitBetweenCycles = numberOfMilliSecondsToWaitBetweenCycles;
        }
        public List<Task<int>> MakeRequests(int numberOfRequests)
        {
            List<Task<int>> requests = new List<Task<int>>();
            for (int i = 0; i < numberOfRequests; i++)
            {
                var request = webRequestHandler.MakeRequest();
                requests.Add(request);
            }
            return requests;
        }
        public Task<List<Task<int>>> MakeThisManyRequestsPerCycle(int numberOfRequestsToMakePerCycle, int numberOfCyclesToRun)
        {
            return Task.Run<List<Task<int>>>(() => {
                var responseCodes = new List<Task<int>>();
                for (int i = 0; i < numberOfCyclesToRun; i++)
                {
                    var requests = MakeRequests(numberOfRequestsToMakePerCycle);
                    responseCodes.AddRange(requests);
                    Thread.Sleep(numberOfMilliSecondsToWaitBetweenCycles);
                }
                return responseCodes;
            });
        }
    }
}