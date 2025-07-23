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
    public class TransferTestConfiguration : IEntityTypeConfiguration<TransferTest>
    {
        public void Configure(EntityTypeBuilder<TransferTest> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.NewOdor)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.SessionToSuccess)
                .IsRequired();

            builder.Property(t => t.WasSuccessful)
                .IsRequired();

            builder.Property(t => t.Notes)
                .HasMaxLength(1000);

            builder.HasOne(t => t.Session)
                .WithMany(s => s.TransferTests)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
