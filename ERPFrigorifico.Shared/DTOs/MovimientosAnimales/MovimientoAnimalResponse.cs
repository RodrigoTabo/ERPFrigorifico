using ERPFrigorifico.Shared.Enums;


namespace ERPFrigorifico.Shared.DTOs.MovimientosAnimales
{
    public class MovimientoAnimalResponse
    {
        public int Id { get; set; }
        public int? CorralId { get; set; }
        public int? FaenaId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public TipoMovimiento? tipoMovimiento { get; set; }
    }
}
