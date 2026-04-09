using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Operario
{
    public class OperarioResponse
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public bool CarnetVencido { get; set; }
    }
}
