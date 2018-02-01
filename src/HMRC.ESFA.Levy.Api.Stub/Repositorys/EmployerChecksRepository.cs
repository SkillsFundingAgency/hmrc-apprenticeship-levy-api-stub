using Dapper;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Sql.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HMRC.ESFA.Levy.Api.Stub.Models;
using HMRC.ESFA.Levy.Api.Stub.Repositorys;

namespace HMRC.ESFA.Levy.Api.Stub.Data
{
    public class EmployerChecksRepository : BaseRepository, IEmployerChecksRepository
    {
        public EmployerChecksRepository(string connectionString, ILog logger) : base(connectionString, logger)
        {
        }

        public async Task<EmployerStatusExtended> GetEmploymentStatus(string empRef, string nino)
        {
            var results = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);
                parameters.Add("@nino", nino, DbType.String);

                return await c.QueryAsync<EmployerStatusExtended>(
                    sql: "[employer_info].[GetEmploymentStatus]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            if (results.Any())
                return results.First();

            return null;
        }

        public async Task<EmployerStatusExtended> GetEmploymentStatusInDateRange(string empRef, string nino, DateTime? fromDate = null,
           DateTime? toDate = null)
        {
            if (!toDate.HasValue)
            {
                toDate = DateTime.MaxValue;
            }

            var results = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);
                parameters.Add("@nino", nino, DbType.String);
                parameters.Add("@fromDate", fromDate, DbType.DateTime);
                parameters.Add("@toDate", toDate, DbType.DateTime);

                return await c.QueryAsync<EmployerStatusExtended>(
                    sql: "[employer_info].[GetEmploymentStatusByDate]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            if (results.Any())
                return results.First();

            return null;
        }
    }
}