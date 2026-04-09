using ERPFrigorifico.Application.Interfaces.Operarios;
using ERPFrigorifico.Shared.DTOs.Operario;

namespace ERPFrigorifico.Application.Services
{
    public class OperarioService(IOperarioRepository operarioRepository): IOperarioService
    {

        private readonly IOperarioRepository _operarioRepository = operarioRepository;

        public async Task<List<OperarioResponse>> ListarOperarios()
        {
            var query = await _operarioRepository.GetAllOperariosHabilitados();

            var consultafinal = query.Select(o => new OperarioResponse
            {
                Id = o.Id,
                NombreCompleto = o.Nombre + " " + o.Apellido,
                DNI = o.DNI,
                CarnetVencido = o.CarnetVencido,
            }).ToList();

            return consultafinal;

        }
    }
}
