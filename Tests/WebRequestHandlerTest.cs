using Xunit;
using mParticle.LoadGenerator;
using System.Threading.Tasks;

namespace Tests
{
    public class WebRequestHandlerTest
    {
        [Fact]
        public async void TestMakeRequest()
        {
            IWebRequestHandler webRequestHandler = new WebRequestHandler();
            var responseCode = await webRequestHandler.MakeRequest();
            Assert.Equal(200, responseCode);
        }
    }
}