using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public class LevyDeclarationDataContext : DbContext, ILevyDeclarationDataContext
    {
        public DbSet<Declaration> Declarations { get; set; }

        public LevyDeclarationDataContext()
        {
        }

        public LevyDeclarationDataContext(DbContextOptions<LevyDeclarationDataContext> options)
            : base(options)
        {
        }

    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.ApplyConfiguration(new Config.EmployerReference());

    //    base.OnModelCreating(modelBuilder);
    //}
}
