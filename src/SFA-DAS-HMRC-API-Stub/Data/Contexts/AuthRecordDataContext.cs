using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public class AuthRecordDataContext : DbContext, IAuthRecordDataContext
    {
        public AuthRecordDataContext(DbContextOptions<AuthRecordDataContext> options)
           : base(options)
        {
        }

        public DbSet<AuthRecord> AuthRecords { get; set; }
    }
}
