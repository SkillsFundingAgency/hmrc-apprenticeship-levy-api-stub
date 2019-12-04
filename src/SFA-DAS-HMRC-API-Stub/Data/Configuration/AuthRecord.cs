using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain = SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Configuration
{
    public class AuthRecord : IEntityTypeConfiguration<Domain.AuthRecord>
    {
        public void Configure(EntityTypeBuilder<Domain.AuthRecord> builder)
        {
            builder.ToTable("AuthRecord");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.GatewayId).HasColumnName(@"GatewayId").HasColumnType("string").HasMaxLength(10).IsRequired();
            builder.Property(x => x.ClientId).HasColumnName(@"ClientId").HasColumnType("string").HasMaxLength(30).IsRequired();
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.AccessToken).HasColumnName(@"AccessToken").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            builder.Property(x => x.RefreshToken).HasColumnName(@"RefreshToken").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName(@"CreatedAt").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.RefreshedAt).HasColumnName(@"RefreshedAt").HasColumnType("datetime");
            builder.Property(x => x.Scope).HasColumnName(@"Scope").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.IsPrivileged).HasColumnName(@"IsPrivileged").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ExpiresIn).HasColumnName(@"ExpiresIn").HasColumnType("int").IsRequired();
        }
    }
}
