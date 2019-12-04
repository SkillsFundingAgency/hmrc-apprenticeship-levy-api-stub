using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class LevyDeclarationRepository : ILevyDeclarationRepository
    {
        private readonly ILevyDeclarationDataContext _levyDeclarationDataContext;
        private readonly ILogger<LevyDeclarationRepository> _logger;

        public LevyDeclarationRepository(
            ILevyDeclarationDataContext levyDeclarationDataContext,
            ILogger<LevyDeclarationRepository> logger)
        {
            _levyDeclarationDataContext = levyDeclarationDataContext ?? throw new ArgumentException("levyDeclarationContext cannot be null");
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        //public async Task<IEnumerable<Declaration>> GetByEmpRef(
        //    string empRef, 
        //    DateTime submissionTime,
        //    DateTime? fromDate = null,
        //    DateTime? toDate = null)
        //{
        //    _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}, submissionTime: {submissionTime}");

        //    // list filtered on date-range

        //    var query = _levyDeclarationDataContext.Declaration
        //        .Where(ld => ld.EmpRef == empRef && ld.SubmissionTime == submissionTime)
        //        .Where(ld => fromDate.Value.Date >= ld.FromDate.Value.Date && toDate.Value.Date < ld.ToDate.Value.Date)
        //        ;

        //    var retVal = new GetLevyDeclarationResponse();
        //    return retVal.Declarations = query.ToList();

        //    //return await query.FirstOrDefaultAsync();
        //}

        public async Task<Declaration> GetByEmpRef(
            string empRef, 
            DateTime fromDate,
            DateTime toDate)
        {
                _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            // list filtered on date-range

            return await _levyDeclarationDataContext.Declarations
                .Where(ld => ld.EmpRef == empRef)
                .Where(ld => ld.SubmissionTime.Date >= fromDate.Date && ld.SubmissionTime.Date < toDate.Date)
                .FirstOrDefaultAsync()
                ;

        }
    }
}
