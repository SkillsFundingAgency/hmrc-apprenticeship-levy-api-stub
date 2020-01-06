using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IAuthRequestRepository
    {
        Task Insert(AuthRequest authRequest);
        Task<AuthRequest> GetAuthRequestById(long id);
        Task<bool> DeleteAuthRequestById(long id);
    }
}