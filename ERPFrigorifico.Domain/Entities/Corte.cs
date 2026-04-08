using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class Corte
    {
        public int Id { get; set; }
        public int CanalId { get; set; }
        public Canal Canal { get; set; } = null!;
        public TipoCorte TipoCorte { get; set; } = TipoCorte.MediaResIzq;
        public decimal Peso { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
