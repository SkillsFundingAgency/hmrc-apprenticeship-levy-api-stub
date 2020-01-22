using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IAuthCodeRepository
    {
        Task Insert(AuthCode authCode);
        Task<AuthCode> GetByCode(string code);
        Task<bool> DeleteByCode(string code);
    }
}