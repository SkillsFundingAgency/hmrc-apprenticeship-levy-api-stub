using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Services
{
    public interface IAuthenticate
    {
        Task<AuthResponse> IsAuthenticated(string token);
        Task<bool> IsAuthorized(string gatewayId, string empRef);
        Task<GatewayUser> Validate(string gatewayId, string password);
    }
}