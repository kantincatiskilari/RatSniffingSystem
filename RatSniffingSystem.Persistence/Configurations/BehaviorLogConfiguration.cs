using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatSniffingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Configurations
{
    public class BehaviorLogConfiguration : IEntityTypeConfiguration<BehaviorLog>
    {
        public void Configure(EntityTypeBuilder<BehaviorLog> builder)
        {
            builder.HasKey(bl => bl.Id);

            builder.Property(bl => bl.BehaviorType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(bl => bl.TimeObserved)
                .IsRequired();

            builder.Property(bl => bl.Notes)
                .HasMaxLength(500);

            builder.HasOne(bl => bl.Session)
                .WithMany(s => s.BehaviorLogs)
                .HasForeignKey(bl => bl.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
