using Dapper;
using HMRC.ESFA.Levy.Api.Types;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Sql.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HMRC.ESFA.Levy.Api.Stub.Data
{
    public class EmployerChecksRepository : BaseRepository
    {
        public EmployerChecksRepository(string connectionString, ILog logger) : base(connectionString, logger)
        {
        }

        public async Task<EmploymentStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            try
            {
                EmploymentStatus result;

                if (fromDate.HasValue)
                {
                    result = await GetEmploymentStatusInDateRange(empRef, nino, fromDate, toDate);
                }
                else
                {
                    result = await GetEmploymentStatus(empRef, nino);
                }

                if (result==null)
                {
                    result = new EmploymentStatus
                    {
                        Employed = false,
                        Empref = empRef,
                        Nino = nino,
                    };
                }

                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<EmploymentStatus> GetEmploymentStatus(string empRef, string nino)
        {
            var results = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);
                parameters.Add("@nino", nino, DbType.String);

                return await c.QueryAsync<EmploymentStatus>(
                    sql: "[employer_info].[GetEmploymentStatus]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            if (results.Any())
                return results.First();

            return null;
        }

        public async Task<EmploymentStatus> GetEmploymentStatusInDateRange(string empRef, string nino, DateTime? fromDate = null,
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

                return await c.QueryAsync<EmploymentStatus>(
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