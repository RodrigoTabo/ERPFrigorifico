using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Domain.Entities
{
    public class CamaraFrio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Temperatura { get; set; }

        public List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
