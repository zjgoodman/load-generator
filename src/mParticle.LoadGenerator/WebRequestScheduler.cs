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

        private volatile Boolean continueRunning = true;

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
        public Task<List<int>> MakeThisManyRequestsPerCycle(int numberOfRequestsToMakePerCycle, int numberOfCyclesToRun)
        {
            return Task.Run<List<int>>(() => {
                var responseCodes = new List<Task<int>>();
                for (int i = 0; i < numberOfCyclesToRun && continueRunning; i++)
                {
                    Console.WriteLine("Making requests...");
                    var requests = MakeRequests(numberOfRequestsToMakePerCycle);
                    responseCodes.AddRange(requests);
                    Thread.Sleep(numberOfMilliSecondsToWaitBetweenCycles);
                }
                return AsyncUtils.FlattenListOfTasks(responseCodes);
            });
        }

        public void stop()
        {
            continueRunning = false;
        }
    }
}