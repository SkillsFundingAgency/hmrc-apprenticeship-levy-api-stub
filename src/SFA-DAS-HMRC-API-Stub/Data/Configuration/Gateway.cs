using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain = SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Configuration
{
    public class Gateway : IEntityTypeConfiguration<Domain.GatewayUser>
    {
        public void Configure(EntityTypeBuilder<Domain.GatewayUser> builder)
        {
            builder.ToTable("GatewayUsers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.GatewayId).HasColumnName(@"GatewayId").HasColumnType("string").HasMaxLength(10).IsRequired();
            builder.Property(x => x.EmpRef).HasColumnName(@"EmpRef").HasColumnType("string").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Require2SV).HasColumnName(@"Require2SV").HasColumnType("bit");
            builder.Property(x => x.Password).HasColumnName(@"Password").HasColumnType("string").HasMaxLength(50).IsRequired();
        }
    }
}
