using ERPFrigorifico.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class OperarioConfig : IEntityTypeConfiguration<Operario>
    {
        public void Configure(EntityTypeBuilder<Operario> b)
        {
            //FK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);
            b.Property(b => b.Apellido).IsRequired().HasMaxLength(50);
            b.Property(b => b.DNI).IsRequired().HasMaxLength(12);
            b.Property(b => b.CarnetVencido).IsRequired();
            b.Property(b => b.FechaCarnetVencido).IsRequired();

            //FK
            b.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(b => b.Ingresos)
                .WithOne(o => o.Operario)
                .HasForeignKey(o => o.OperarioId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
