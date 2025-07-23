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
    public class TrialOdorConfiguration : IEntityTypeConfiguration<TrialOdor>
    {
        public void Configure(EntityTypeBuilder<TrialOdor> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.OdorType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.PositionIndex)
                .IsRequired();

            builder.Property(t => t.IsFalsePositive)
                .IsRequired();

            builder.HasOne(t => t.Trial)
                .WithMany(t => t.OdorPositions)
                .HasForeignKey(t => t.TrialId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Odor)
                .WithMany(o => o.TrialUsages)
                .HasForeignKey(t => t.OdorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
