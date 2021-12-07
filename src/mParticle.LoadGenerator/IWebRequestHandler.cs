using System.Threading.Tasks;

namespace mParticle.LoadGenerator
{
    public interface IWebRequestHandler
    {
         Task<int> MakeRequest();
    }
}