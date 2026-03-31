using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class ProveedorConfig : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> b)
        {
            //PK
            b.HasKey(p => p.Id);

            //Propiedades
            b.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            b.Property(p => p.Direccion).IsRequired().HasMaxLength(100);
            b.Property(p => p.CUIL).IsRequired();

            //FK
            b.HasMany(i => i.Ingresos)
                .WithOne(p => p.Proveedor)
                .HasForeignKey(p => p.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
