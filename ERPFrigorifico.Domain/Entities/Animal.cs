using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public TipoAnimal TipoAnimal { get; set; } = TipoAnimal.Vaca;
        public decimal PesoIngreso { get; set; }
        public int IngresoId { get; set; }
        public Ingreso Ingreso { get; set; } = null!;
        public int? FaenaId { get; set; }
        public Faena? Faena { get; set; }
        public List<MovimientoAnimal> MovimientosAnimal { get; set; } = new List<MovimientoAnimal>();

    }
}
