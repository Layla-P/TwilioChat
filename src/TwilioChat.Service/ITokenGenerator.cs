using System.Threading.Tasks;

namespace TwilioChat.Service
{
    public interface ITokenGenerator
    {
        Task<string> Generate(string identity, string endpointId);
    }
}
