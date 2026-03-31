using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class CamaraFrioConfig : IEntityTypeConfiguration<CamaraFrio>
    {
        public void Configure(EntityTypeBuilder<CamaraFrio> b)
        {
            //PK 
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);
            b.Property(b => b.Temperatura).IsRequired().HasPrecision(5, 2);

            //FK
            b.HasMany(b => b.Stocks)
                .WithOne(s => s.CamaraFrio)
                .HasForeignKey(b => b.CamaraFrioId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
