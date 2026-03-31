using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class CamionConfig : IEntityTypeConfiguration<Camion>
    {
        public void Configure(EntityTypeBuilder<Camion> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Modelo).IsRequired().HasMaxLength(20);
            b.Property(b => b.Marca).IsRequired().HasMaxLength(20);

            //FK
            b.HasMany(b => b.Ingresos)
                .WithOne(c => c.Camion)
                .HasForeignKey(c => c.CamionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
