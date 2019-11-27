using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Configuration
{
    public class EmployerReference : IEntityTypeConfiguration<Domain.EmployerReference>
    {
        public void Configure(EntityTypeBuilder<Domain.EmployerReference> builder)
        {
            builder.ToTable("EmployerReference");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.EmpRef).HasColumnName(@"EmpRef").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Data).HasColumnName(@"Data").HasColumnType("varchar(max)").IsRequired();
        }
    }
}
