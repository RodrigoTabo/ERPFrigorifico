using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Faena
    {
        public int Id { get; set; }
        public DateTime FechaProduccion { get; set; }
        public List<Canal> Canales { get; set; } = new List<Canal>();
        public List<Animal> Animales { get; set; } = new List<Animal>();
        public List<MovimientoAnimal> MovimientosAnimal { get; set; } = new List<MovimientoAnimal>();

    }
}
