using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int CorteId { get; set; }
        public Corte Corte { get; set; } = null!;
        public int CamaraFrioId { get; set; }
        public CamaraFrio CamaraFrio { get; set; } = null!;
        public decimal PesoDisponible { get; set; }

    }
}
