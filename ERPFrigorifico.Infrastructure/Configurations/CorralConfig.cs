using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class CorralConfig : IEntityTypeConfiguration<Corral>
    {
        public void Configure(EntityTypeBuilder<Corral> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);

            //FK
            b.HasMany(b => b.MovimientosAnimal)
                .WithOne(c => c.Corral)
                .HasForeignKey(c => c.CorralId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
