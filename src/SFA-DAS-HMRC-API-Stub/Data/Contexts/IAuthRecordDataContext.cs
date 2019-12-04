using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public interface IAuthRecordDataContext
    {
        DbSet<AuthRecord> AuthRecords { get; set; }
    }
}
