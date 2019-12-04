using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Repositories
{
    public class EmployerChecksRepository : IEmployerChecksRepository
    {
        private readonly IEmployerDataContext _employerDataContext;
        private readonly ILogger<EmployerChecksRepository> _logger;

        public EmployerChecksRepository(
            IEmployerDataContext employerDataContext,
            ILogger<EmployerChecksRepository> logger)
        {
            _employerDataContext = employerDataContext ?? throw new ArgumentException("employerDataContext cannot be null");
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<EmployerStatus> GetEmploymentStatus(
            string empRef,
            string nino,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            _logger.LogDebug($"Getting employment status in date range, empRef: {empRef}, nino: {nino}, fromDate: {fromDate}, toDate: {toDate}");

            var query = _employerDataContext.EmployerStatus
                .Where(es => es.EmpRef == empRef && es.Nino == nino)
                .Where(es => fromDate.Value.Date >= es.FromDate.Value.Date && toDate.Value.Date < es.ToDate.Value.Date)
            ;

            return await query.FirstOrDefaultAsync();
        }
    }
}
 