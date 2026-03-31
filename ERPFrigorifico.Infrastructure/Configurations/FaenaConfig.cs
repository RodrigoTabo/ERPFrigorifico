using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class FaenaConfig : IEntityTypeConfiguration<Faena>
    {
        public void Configure(EntityTypeBuilder<Faena> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propíedades
            b.Property(b => b.FechaProduccion).IsRequired();
            b.Property(b => b.AnimalId).IsRequired();

            //Indices
            b.HasIndex(b => b.AnimalId);

            //FK
            b.HasMany(b => b.Canales)
                .WithOne(f => f.Faena)
                .HasForeignKey(f => f.FaenaId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(b => b.MovimientosAnimal)
                .WithOne(f => f.Faena)
                .HasForeignKey(f => f.FaenaId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
