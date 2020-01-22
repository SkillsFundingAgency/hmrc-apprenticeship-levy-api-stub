using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class FractionsRepository : BaseRepository, IFractionsRepository, IFractionsCalcDateRepository
    {
        private readonly ILogger<FractionsRepository> _logger;
        private readonly IMongoCollection<RootObject> _fractions;
        private readonly IMongoCollection<FractionCalculationDate> _fractionCalcDate;

        public FractionsRepository(IMongoDatabase database, ILogger<FractionsRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _fractions = Database.GetCollection<RootObject>("fractions");
            _fractionCalcDate = Database.GetCollection<FractionCalculationDate>("fraction_calculation_date");
        }
      
        public async Task<RootObject> GetByEmpRef(string empRef, DateTime fromDate, DateTime toDate)
        {

            _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            var fractions = _fractions
                .Find(f => f.EmpRef == empRef)
                .ToList()
                .SelectMany(f => f.FractionCalculations
                .Where(fd => fd.CalculatedAt.Date >= fromDate.Date && fd.CalculatedAt.Date < toDate.Date))
            ;

            return new RootObject()
            {
                EmpRef = empRef,
                FractionCalculations = fractions.OrderBy(f => f.CalculatedAt).ToList()
            };
        }

        public async Task<FractionCalculationDate> GetLastCalcDate()
        {
            _logger.LogDebug("Getting lastCalculationDate");

            return _fractionCalcDate.Find(fractionCalculationDate => true)
                .ToList()
                .FirstOrDefault();
        }
    }
}
