using System;
using System.Threading;
using System.Threading.Tasks;

namespace mParticle.LoadGenerator
{
    public class WebRequestHandler : IWebRequestHandler
    {
        public Task<int> MakeRequest()
        {
            return Task.FromResult(200); // TODO
        }
    }
}