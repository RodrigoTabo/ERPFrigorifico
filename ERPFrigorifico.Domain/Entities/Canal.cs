using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Canal
    {
        public int Id { get; set; }
        public int FaenaId { get; set; }
        public Faena Faena { get; set; } = null!;
        public decimal Peso { get; set; }
        public List<Corte> Cortes { get; set; } = new List<Corte>();
    }
}
