
using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class MovimientoAnimalService(
        IMovimientoAnimalRepository movimientoAnimalRepository,
        IAnimalRepository animalRepository,
        IFaenaService faenaService
        ) : IMovimientoAnimalService
    {

        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IFaenaService _faenaService = faenaService;
        private readonly IMovimientoAnimalRepository _movimientoAnimalRepository = movimientoAnimalRepository;
        public async Task<PagedResult<MovimientoAnimalResponse>> GetAllMovimientosAnimales(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento)
        {
            var query = await _movimientoAnimalRepository.GetMovimientosPaginados(pageIndex, pageSize, tipoMovimiento);

            var ultimaConsulta = query.Items
                .Select(m => new MovimientoAnimalResponse
                {
                    Id = m.Id,
                    animalId = m.AnimalId,
                    CorralId = m.CorralId,
                    FaenaId = m.FaenaId,
                    FechaMovimiento = m.FechaMovimiento,
                    tipoMovimiento = m.TipoMovimiento,
                }).ToList();

            return new PagedResult<MovimientoAnimalResponse>
            {
                Items = ultimaConsulta,
                TotalCount = query.TotalCount
            };
        }

        public async Task EnviarAnimales(List<int> animalIds)
        {
            var animales = await _animalRepository.GetByIds(animalIds);

            ValidarAnimalExistente(animales, animalIds);

            var entradaCorral = await _movimientoAnimalRepository.GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.EntradaCorral);

            var animalesEnCorralesIds = entradaCorral.Select(a => a.Id).ToList();

            var hayInvalidos = animalIds.Any(id => !animalesEnCorralesIds.Contains(id));
            var Faena = await _movimientoAnimalRepository.GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.Faena);

            //Si hay algun animal que no este en corral, lanzamos una excepcion
            ValidarAnimalesEnCorral(hayInvalidos);


            if (entradaCorral is not null)
            {
               await _faenaService.EnviarAnimalesAFaena(entradaCorral);

            }
            else if (_movimientoAnimalRepository.GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.Ingreso) is not null)
            {
                //Enviar a algun lado que todavia no tengo especificado.
            }

        }

        //Metodos privados de validacion

        private void ValidarAnimalesEnCorral(bool hayInvalidos)
        {
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en corral");
        }

        private void ValidarAnimalExistente(List<Animal> animales, List<int> animalIds)
        {
            if (animales.Count != animalIds.Count)
                throw new NotFoundException("Algunos animales no existen");
        }
    }
}
