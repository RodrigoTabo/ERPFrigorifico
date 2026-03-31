using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class AnimalConfig : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.PesoIngreso).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.IngresoId).IsRequired();

            //Indices
            b.HasIndex(b => b.IngresoId);

            //FK
            b.HasMany(b => b.Faenas)
                .WithOne(a => a.Animal)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(b => b.MovimientosAnimal)
                .WithOne(a => a.Animal)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
