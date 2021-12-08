using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public interface IWebRequestScheduler
    {
        Task<List<Task<int>>> MakeThisManyRequestsPerCycle(int numberOfRequestsToMakePerCycle, int numberOfCyclesToRun);
        void stop();
    }
}