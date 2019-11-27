using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public interface IEmployerReferenceDataContext
    {
        DbSet<EmployerReference> EmployerReference { get; set; }
    }
}