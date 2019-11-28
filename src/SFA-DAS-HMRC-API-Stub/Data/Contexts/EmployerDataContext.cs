using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Domain;
using Config = SFA.DAS.HMRC.API.Stub.Data.Configuration;

namespace SFA.DAS.HMRC.API.Stub.Data.Contexts
{
    public class EmployerDataContext : DbContext, IEmployerDataContext
    {
        public DbSet<EmployerStatus> EmployerStatus { get; set; }        

        public EmployerDataContext()
        {
        }

        public EmployerDataContext(DbContextOptions<EmployerDataContext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Config.EmployerStatus());


            base.OnModelCreating(modelBuilder);
        }
    }
}

