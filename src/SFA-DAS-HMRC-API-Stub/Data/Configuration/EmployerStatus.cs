using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain = SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Configuration
{
    public class EmployerStatus : IEntityTypeConfiguration<Domain.EmployerStatus>
    {
        public void Configure(EntityTypeBuilder<Domain.EmployerStatus> builder)
        {
            builder.ToTable("EmploymentStatus");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.EmpRef).HasColumnName(@"EmpRef").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Employed).HasColumnName(@"Employed").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Nino).HasColumnName(@"Nino").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.FromDate).HasColumnName(@"FromDate").HasColumnType("datetime");
            builder.Property(x => x.ToDate).HasColumnName(@"ToDate").HasColumnType("datetime");
            builder.Property(x => x.HttpStatusCode).HasColumnName(@"HttpStatusCode").HasColumnType("smallint");

        }
    }
}
