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
    public class OdorConfiguration : IEntityTypeConfiguration<Odor>
    {
        public void Configure(EntityTypeBuilder<Odor> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Description)
                .HasMaxLength(250);

            builder.Property(o => o.OdorCategory)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(o => o.IsHazardous)
                .IsRequired();

            builder.Property(o => o.ExternalCode)
                .HasMaxLength(50);

           
        }
    }
}
