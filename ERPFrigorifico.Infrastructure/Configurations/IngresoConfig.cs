using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class IngresoConfig : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.PesoBruto).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.PesoNeto).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.PesoTara).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.FechaIngreso).IsRequired();

            //Indices
            b.HasIndex(b => b.ProveedorId);
            b.HasIndex(b => b.CamionId);
            b.HasIndex(b => new { b.ProveedorId, b.FechaIngreso });
            b.HasIndex(b => new { b.CamionId, b.FechaIngreso });

            //FK
            b.HasMany(b => b.Animales)
                .WithOne(i => i.Ingreso)
                .HasForeignKey(i => i.IngresoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
