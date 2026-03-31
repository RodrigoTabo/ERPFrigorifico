using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CUIL { get; set; }
        public string Direccion { get; set; } = null!;
        public DateTime? EliminadoEn { get; set; }
        public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    }
}
