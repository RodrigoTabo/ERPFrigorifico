using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.MovimientosAnimales
{
    public class MovimientoAnimalByIdResponse
    {
        public int AnimalId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
    }
}
