using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class EmployerReferenceRepository : IEmployerReferenceRepository
    {
        private readonly IEmployerReferenceDataContext _employerReferenceDataContext;
        private readonly ILogger<EmployerReferenceRepository> _logger;

        public EmployerReferenceRepository(
            IEmployerReferenceDataContext employerReferenceDataContext,
            ILogger<EmployerReferenceRepository> logger)
        {
            _employerReferenceDataContext = employerReferenceDataContext ?? throw new ArgumentException("employerReferenceDataContext cannot be null");
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<EmployerReference> GetEmployerReference(string empRef)
        {
            _logger.LogDebug($"GetEmployerReference: {empRef}");

            return await _employerReferenceDataContext.EmployerReference
                .Where(erf => erf.EmpRef == empRef)
                .SingleOrDefaultAsync()
            ;
        }
    }
}
