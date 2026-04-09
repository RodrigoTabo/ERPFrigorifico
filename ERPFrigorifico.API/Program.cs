using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.CamarasFrio;
using ERPFrigorifico.Application.Interfaces.Canales;
using ERPFrigorifico.Application.Interfaces.Corrales;
using ERPFrigorifico.Application.Interfaces.Cortes;
using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Application.Interfaces.Proveedores;
using ERPFrigorifico.Application.Interfaces.Stocks;
using ERPFrigorifico.Application.Services;
using ERPFrigorifico.Infrastructure.Data;
using ERPFrigorifico.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //SERVICIO FLUENTVALIDATOR
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);


            // SERVICIOS Y REPOSITORIOS DE LA APLICACION.
            builder.Services.AddScoped<IIngresoRepository, IngresoRepository>();
            builder.Services.AddScoped<IIngresoService, IngresoService>();
            builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
            builder.Services.AddScoped<ICamaraFrioRepository, CamaraFrioRepository>();
            builder.Services.AddScoped<ICanalRepository, CanalRepository>();
            builder.Services.AddScoped<ICanalService, CanalService>();
            builder.Services.AddScoped<ICorralService, CorralService>();
            builder.Services.AddScoped<ICorteRepository, CorteRepository>();
            builder.Services.AddScoped<ICorteService, CorteService>();
            builder.Services.AddScoped<IFaenaRepository, FaenaRepository>();
            builder.Services.AddScoped<IFaenaService, FaenaService>();
            builder.Services.AddScoped<IMovimientoAnimalService, MovimientoAnimalService>();
            builder.Services.AddScoped<IMovimientoAnimalRepository, MovimientoAnimalRepository>();
            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
            builder.Services.AddScoped<IProveedorService, ProveedorService>();

            // SERVICO DE LA CONEXION.
            builder.Services
                .AddDbContext<ERPFrigorificoDbContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("ERPFrigorificoDbContext")));

            builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            // SERVICIO DE IDENTITYCORE
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ERPFrigorificoDbContext>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //Servicio SwaggerGen
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
