using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Corrales;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class CorralService(IAnimalRepository animalRepository,
        IMovimientoAnimalRepository movimientoAnimalRepository,
        IUnitOfWorkRepository unitOfWorkRepository) : ICorralService
    {

        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IMovimientoAnimalRepository _movimientoAnimalRepository = movimientoAnimalRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task EnviarAnimalesACorral(List<int> animalIds, int corralId)
        {

            var animales = await _animalRepository.GetByIds(animalIds);

            if (animales.Count != animalIds.Count)
                throw new NotFoundException("Algunos animales no existen");

            var animalesEnIngreso = await _movimientoAnimalRepository
                .GetAnimalesPorUltimoMovimiento(TipoMovimiento.Ingreso);

            var animalesEnIngresoIds = animalesEnIngreso.Select(a => a.Id).ToList();

            //Verificamos si hay algun animal que no este en ingreso
            var hayInvalidos = animalIds.Any(id => !animalesEnIngresoIds.Contains(id));

            //Si hay algun animal que no este en ingreso, lanzamos una excepcion
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en ingreso");

            foreach (var animal in animales)
            {
                var movimiento = new MovimientoAnimal
                {
                    AnimalId = animal.Id,
                    CorralId = corralId,
                    TipoMovimiento = TipoMovimiento.EntradaCorral,
                    FechaMovimiento = DateTime.UtcNow
                };

                animal.MovimientosAnimal.Add(movimiento);
            }

            await _unitOfWorkRepository.SaveChangesAsync();

        }

    }
}
