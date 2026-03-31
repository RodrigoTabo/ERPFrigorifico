using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Faena
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public DateTime FechaProduccion { get; set; }
        public List<Canal> Canales { get; set; } = new List<Canal>();
        public List<MovimientoAnimal> MovimientosAnimal { get; set; } = new List<MovimientoAnimal>();

    }
}
