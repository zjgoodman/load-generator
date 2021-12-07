using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public class WebRequestScheduler
    {
        public Task<int> MakeRequest()
        {
            // TODO make actual web request
            return Task.Run<int>(() => 200);
        }
        public List<Task<int>> MakeRequests(int numberOfRequests)
        {
            List<Task<int>> requests = new List<Task<int>>();
            for (int i = 0; i < numberOfRequests; i++)
            {
                requests.Add(MakeRequest());
            }
            return requests;
        }
        public Task<List<Task<int>>> MakeThisManyRequestsPerSecond(int numberOfRequestsToMakePerSecond, int numberOfSecondsToRun)
        {
            return Task.Run<List<Task<int>>>(() => {
                var responseCodes = new List<Task<int>>();
                for (int i = 0; i < numberOfSecondsToRun; i++)
                {
                    var requests = MakeRequests(numberOfRequestsToMakePerSecond);
                    responseCodes.AddRange(requests);
                    Thread.Sleep(1000);
                }
                return responseCodes;
            });
        }
    }
}