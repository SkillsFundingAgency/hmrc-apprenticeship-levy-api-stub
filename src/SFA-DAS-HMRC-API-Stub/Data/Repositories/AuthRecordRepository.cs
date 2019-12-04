using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class AuthRecordRepository : IAuthRecordRepository
    {
        private readonly IAuthRecordDataContext _authRecordDataContext;

        public AuthRecordRepository(IAuthRecordDataContext authRecordDataContext)
        {
            _authRecordDataContext = authRecordDataContext;
        }

        public async Task<IEnumerable<AuthRecord>> GetAuthRecords(string token)
        {
            return await _authRecordDataContext.AuthRecords
                .Where(auth => auth.AccessToken == token && (auth.RefreshedAt.AddMilliseconds(auth.ExpiresIn * 1000) >= DateTime.UtcNow))
                .ToListAsync()
            ;
        }
    }
}
