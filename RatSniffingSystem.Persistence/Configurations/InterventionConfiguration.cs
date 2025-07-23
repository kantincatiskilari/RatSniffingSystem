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
    public class InterventionConfiguration : IEntityTypeConfiguration<Intervention>
    {
        public void Configure(EntityTypeBuilder<Intervention> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Substance)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Dose)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(i => i.AppliedAt)
                .IsRequired();

            builder.Property(i => i.InterventionType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(i => i.Notes)
                .HasMaxLength(1000);

            builder.HasOne(i => i.Session)
                .WithMany(s => s.Interventions)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
