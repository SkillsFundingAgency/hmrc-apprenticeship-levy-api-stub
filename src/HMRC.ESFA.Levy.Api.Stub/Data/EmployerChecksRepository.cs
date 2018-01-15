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

        public async Task<EmploymentStatus> GetEmploymentStatus(string empRef)
        {
            var results = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);

                return await c.QueryAsync<EmploymentStatus>(
                    sql: "[employer_info].[GetEmploymentStatus]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });
            return results.First();
        }
    }
}