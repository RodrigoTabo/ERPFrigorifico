using ERPFrigorifico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ERPFrigorifico.Infrastructure.Configurations
{
    public class CanalConfig : IEntityTypeConfiguration<Canal>
    {
        public void Configure(EntityTypeBuilder<Canal> b)
        {
            //PK
            b.HasKey(b => b.Id);

            //Propiedades
            b.Property(b => b.Peso).IsRequired().HasPrecision(18, 2);
            b.Property(b => b.FaenaId).IsRequired();

            //Indices
            b.HasIndex(b => b.FaenaId);

            //FK
            b.HasMany(b => b.Cortes)
                .WithOne(c => c.Canal)
                .HasForeignKey(c => c.CanalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
