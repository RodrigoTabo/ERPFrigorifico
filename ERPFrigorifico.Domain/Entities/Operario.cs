using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Operario
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public bool CarnetVencido { get; set; } = false;
        public DateTime? FechaCarnetVencido { get; set; }
        public DateTime? EliminadoEn { get; set; }
        public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    }
}
