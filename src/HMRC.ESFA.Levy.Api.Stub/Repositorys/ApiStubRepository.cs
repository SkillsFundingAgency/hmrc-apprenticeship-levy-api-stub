using SFA.DAS.NLog.Logger;
using SFA.DAS.Sql.Client;

namespace HMRC.ESFA.Levy.Api.Stub.Repositorys
{
    public class ApiStubRepository : BaseRepository
    {
        public ApiStubRepository(string connectionString, ILog logger) : base(connectionString, logger)
        {
        }
    }
}