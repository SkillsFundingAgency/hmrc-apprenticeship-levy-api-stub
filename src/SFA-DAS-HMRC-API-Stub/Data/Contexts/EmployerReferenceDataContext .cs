using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;
using Config = SFA.DAS.HMRC.API.Stub.Data.Configuration;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public class EmployerReferenceDataContext : DbContext, IEmployerReferenceDataContext
    {
        public DbSet<EmployerReference> EmployerReference { get; set; }

        public EmployerReferenceDataContext()
        {
        }

        public EmployerReferenceDataContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Config.EmployerReference());

            base.OnModelCreating(modelBuilder);
        }
    }
}

