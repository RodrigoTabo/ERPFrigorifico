using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class CorteConfig : IEntityTypeConfiguration<Corte>
    {
        public void Configure(EntityTypeBuilder<Corte> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Peso).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.CanalId).IsRequired();

            //Indices
            b.HasIndex(b => b.CanalId);

            //FK
            b.HasMany(b => b.Stocks)
                .WithOne(c => c.Corte)
                .HasForeignKey(c => c.CorteId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
