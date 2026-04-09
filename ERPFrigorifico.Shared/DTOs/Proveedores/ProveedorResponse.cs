using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Proveedor
{
    public class ProveedorResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cuil { get; set; }
        public string Direccion { get; set; }
    }
}
