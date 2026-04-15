using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Faenas
{
    public class FaenaResponse
    {
        public int Id { get; set; }
        public DateTime FechaProduccion { get; set; }
        public TipoFaena TipoFaena { get; set; }
    }
}
