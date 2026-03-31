using ERPFrigorifico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Ingreso
    {
        public int Id { get; set; }
        public int CamionId { get; set; }
        public Camion Camion { get; set; } = null!;
        public int OperarioId { get; set; }
        public Operario Operario { get; set; } = null!;
        public decimal PesoBruto { get; set; }
        public decimal PesoTara { get; set; }
        public decimal PesoNeto { get; set; }
        public Estado Estado { get; set; } = Estado.Excelente;
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }  
        public List<Animal> Animales { get; set; } = new List<Animal>();

    }
}
