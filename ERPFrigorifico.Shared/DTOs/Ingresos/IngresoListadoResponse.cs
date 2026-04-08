using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Ingresos
{
    public class IngresoListadoResponse
    {
        public string Nombre { get; set; }
        public string Patente { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int MinutosEnPlanta { get; set; }
        public TipoIngreso TipoIngreso { get; set; }
    }
}
