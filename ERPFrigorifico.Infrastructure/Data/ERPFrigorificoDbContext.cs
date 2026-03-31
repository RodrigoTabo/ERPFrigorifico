using ERPFrigorifico.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Data
{
    public class ERPFrigorificoDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public ERPFrigorificoDbContext(DbContextOptions<ERPFrigorificoDbContext> options)
            : base(options) { }

        //DbSets
        public DbSet<Animal> Animales => Set<Animal>();
        public DbSet<CamaraFrio> CamarasFrio => Set<CamaraFrio>();
        public DbSet<Camion> Camiones => Set<Camion>();
        public DbSet<Corral> Corrales => Set<Corral>();
        public DbSet<Corte> Cortes => Set<Corte>();
        public DbSet<Faena> Faenas => Set<Faena>();
        public DbSet<Ingreso> Ingresos => Set<Ingreso>();
        public DbSet<MovimientoAnimal> MovimientosAnimal => Set<MovimientoAnimal>();
        public DbSet<Operario> Operarios => Set<Operario>();
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Stock> Stocks => Set<Stock>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ERPFrigorificoDbContext).Assembly);
        }

    }
}
