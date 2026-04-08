using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Canales;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.Enums;


namespace ERPFrigorifico.Application.Services
{
    public class CanalService(ICanalRepository canalRepository, IUnitOfWorkRepository unitOfWorkRepository) : ICanalService
    {

        private readonly ICanalRepository _canalRepository = canalRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task GenerarMediaReses(int canalId)
        {
            var canal = await _canalRepository.GetCanalById(canalId);

            ExisteCanal(canal);

            CanalTieneCortesGenerados(canal);

            CanalTienePesoAsignado(canal);

            var pesoMediaRes = canal.Peso / 2;

            canal.Cortes.Add(new Corte
            {
                Peso = pesoMediaRes,
                TipoCorte = TipoCorte.MediaResIzq
            });

            canal.Cortes.Add(new Corte
            {
                Peso = pesoMediaRes,
                TipoCorte = TipoCorte.MediaResDer
            });

            await _unitOfWorkRepository.SaveChangesAsync();

        }

        //metodos privados
        private void ExisteCanal(Canal canal)
        {
            if (canal == null)
                throw new NotFoundException("No existe ningun canal en proceso.");
        }

        private void CanalTieneCortesGenerados(Canal canal)
        {
            if (canal.Cortes.Any())
                throw new ConflictException("El canal ya tiene cortes generados.");
        }

        private void CanalTienePesoAsignado(Canal canal)
        {
            if (canal.Peso <= 0)
                throw new ConflictException("El canal no tiene peso asignado");
        }

    }
}
