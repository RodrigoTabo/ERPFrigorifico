using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Corral
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public List<MovimientoAnimal> MovimientosAnimal { get; set; } = new List<MovimientoAnimal>();

    }
}
