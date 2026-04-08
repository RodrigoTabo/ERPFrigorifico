using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Shared.DTOs.Ingresos
{
    public class RegistrarIngresoRequest
    {
        public int? CamionId { get; set; }
        public int? OperarioId { get; set; }
        public int? ProveedorId { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoTara { get; set; }
        public int CantidadAnimales { get; set; }
        public string Patente { get; set; }
        public TipoIngreso tipoIngreso { get; set; }
    };
}
