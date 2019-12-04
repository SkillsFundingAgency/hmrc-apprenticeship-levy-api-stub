using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Configuration
{
    public class Declaration : IEntityTypeConfiguration<Domain.Declaration>
    {
        public void Configure(EntityTypeBuilder<Domain.Declaration> builder)
        {
            builder.ToTable("Declarations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.EmpRef).HasColumnName(@"EmpRef").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.SubmissionTime).HasColumnName(@"SubmissionTime").HasColumnType("DateTime").IsRequired();
            builder.Property(x => x.Data).HasColumnName(@"Data").HasColumnType("varchar(max)").IsRequired();
        }
    }
}
