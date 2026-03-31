using ERPFrigorifico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class MovimientoAnimal
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public int? CorralId { get; set; }
        public Corral? Corral { get; set; } 
        public int? FaenaId { get; set; }
        public Faena? Faena { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; } = TipoMovimiento.Ingreso;
    }
}
