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
    public class FoodIntakeLogConfiguration : IEntityTypeConfiguration<FoodIntakeLog>
    {
        public void Configure(EntityTypeBuilder<FoodIntakeLog> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.AmountInCc)
                .HasPrecision(18,2)
                .IsRequired();

            builder.Property(f => f.TimeGiven)
                .IsRequired(false);

            builder.Property(f => f.Notes)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
