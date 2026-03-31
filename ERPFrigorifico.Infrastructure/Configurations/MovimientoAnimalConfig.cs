using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class MovimientoAnimalConfig : IEntityTypeConfiguration<MovimientoAnimal>
    {
        public void Configure(EntityTypeBuilder<MovimientoAnimal> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.FechaMovimiento).IsRequired();
            b.Property(b => b.AnimalId).IsRequired();

            //Indices
            b.HasIndex(b => new { b.AnimalId, b.FechaMovimiento });
            b.HasIndex(b => new { b.CorralId, b.FechaMovimiento });
            b.HasIndex(b => new { b.FaenaId, b.FechaMovimiento });

        }
    }
}
