using Xunit;
using mParticle.LoadGenerator;
using System.Threading.Tasks;

namespace Tests
{
    public class WebRequestHandlerTest
    {
        [Fact]
        public void TestMakeRequest()
        {
            IWebRequestHandler webRequestHandler = new WebRequestHandler();
            var responseCode = webRequestHandler.MakeRequest();
            Assert.Null(responseCode);
        }
    }
}