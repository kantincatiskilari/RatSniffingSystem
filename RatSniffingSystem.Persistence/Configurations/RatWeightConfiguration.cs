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
    public class RatWeightConfiguration : IEntityTypeConfiguration<RatWeight>
    {
        public void Configure(EntityTypeBuilder<RatWeight> builder)
        {
            builder.HasKey(rw => rw.Id);

            builder.Property(rw => rw.Date)
                .IsRequired();
            builder.Property(rw => rw.WeightInGrams)
                .HasPrecision(18,2)
                .IsRequired();
            builder.Property(rw => rw.Notes)
                .HasMaxLength(500);

            builder.HasOne(rw => rw.Rat)
                .WithMany(r => r.WeightRecords)
                .HasForeignKey(rw => rw.RatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
