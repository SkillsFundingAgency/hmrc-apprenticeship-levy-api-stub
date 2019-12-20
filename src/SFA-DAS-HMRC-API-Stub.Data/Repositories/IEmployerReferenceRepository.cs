using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IEmployerReferenceRepository
    {
        Task<EmployerReference> GetEmployerReference(string empRef);
    }
}