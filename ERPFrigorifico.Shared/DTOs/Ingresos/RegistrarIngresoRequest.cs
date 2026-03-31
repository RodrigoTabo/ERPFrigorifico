using ERPFrigorifico.Domain.Enums;

namespace ERPFrigorifico.Shared.DTOs.Ingresos
{
    public record RegistrarIngresoRequest(int? camionId, int? operarioId, int? proveedorId, decimal pesoBruto, int cantidadAnimales, decimal pesoTara, string patente, TipoIngreso tipoIngreso);
}
