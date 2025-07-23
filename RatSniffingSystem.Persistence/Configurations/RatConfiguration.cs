using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Configurations
{
    public class RatConfiguration : IEntityTypeConfiguration<Rat>
    {
        public void Configure(EntityTypeBuilder<Rat> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(r => r.BirthDate)
                .IsRequired();

            builder.Property(r => r.IsActive)
                .IsRequired();

            builder.Property(r => r.Notes)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(r => r.ProjectTag)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Breed)
                .HasMaxLength(50);

           
        }
    }
}
