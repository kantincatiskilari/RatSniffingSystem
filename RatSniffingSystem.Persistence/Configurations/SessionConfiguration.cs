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
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SessionDate)
                .IsRequired();

            builder.Property(s => s.DurationMinutes)
                .IsRequired();

            builder.Property(s => s.CageCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.MaterialType)
                .HasMaxLength(50);

            builder.Property(s => s.MaterialThawedAt);

            builder.Property(s => s.StartTime)
                   .IsRequired();

            builder.HasOne(s => s.Rat)
              .WithMany(r => r.Sessions)
              .HasForeignKey(s => s.RatId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Trainer)
                   .WithMany(t => t.Sessions)
                   .HasForeignKey(s => s.TrainerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
