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
    public class TrialConfiguration : IEntityTypeConfiguration<Trial>
    {
        public void Configure(EntityTypeBuilder<Trial> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TrialNumber)
                .IsRequired();

            builder.Property(t => t.TargetOdor)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.FirstResponseTime)
                .IsRequired();
            builder.Property(t => t.FirstCorrectTime)
                .IsRequired(false);
            builder.Property(t => t.IsCorrectPositive)
                .IsRequired();
            builder.Property(t => t.IsFalsePositive)
                .IsRequired();
            builder.Property(t => t.IsCorrectNegative)
                .IsRequired();
            builder.Property(t => t.IsFalseNegative)
                .IsRequired();


            builder.HasOne(t => t.Session)
                .WithMany(s => s.Trials)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
