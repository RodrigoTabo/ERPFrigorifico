using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure (EntityTypeBuilder<Stock> b)
        {

            //PK 
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.PesoDisponible).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.CorteId).IsRequired();
            b.Property(b => b.CamaraFrioId).IsRequired();


            //Indices
            b.HasIndex(b => b.CorteId);
            b.HasIndex(b => b.CamaraFrioId);
            b.HasIndex(b => new { b.CorteId, b.CamaraFrioId });

        }
    }
}
