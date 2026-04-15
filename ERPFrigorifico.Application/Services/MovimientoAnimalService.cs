
using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Corrales;
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
        ICorralService corralService,
        IFaenaService faenaService
        ) : IMovimientoAnimalService
    {

        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IFaenaService _faenaService = faenaService;
        private readonly ICorralService _corralService = corralService;
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

        //Estoy llevando los animales del corral a la faena para ser procesados, me falta llevarlos desde ingresos hasta el corral, y despues ahi si. 
        public async Task EnviarAnimales(List<int> animalIds)
        {
            ////Proceso de validacion general
            //var animales = await _animalRepository.GetByIds(animalIds);

            //ValidarAnimalExistente(animales, animalIds);

            var ingreso = await _movimientoAnimalRepository
                .GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.Ingreso);

            var entradaCorral = await _movimientoAnimalRepository
                .GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.EntradaCorral);

            if (ingreso.Any())
            {
                await _corralService.EnviarAnimalesACorral(ingreso);
            }

            if (entradaCorral.Any())
            {
                await _faenaService.EnviarAnimalesAFaena(entradaCorral);
            }
        }

        public async Task<List<MovimientoAnimalByIdResponse>> GetHistorialAnimalById(int id)
        {
            var obtener = await _movimientoAnimalRepository.GetHistorialAnimalById(id);
            return obtener.ToList();
        }


        //Metodos privados de validacion

        private void ValidarAnimalExistente(List<Animal> animales, List<int> animalIds)
        {
            if (animales.Count != animalIds.Count)
                throw new NotFoundException("Algunos animales no existen");
        }

    }
}
