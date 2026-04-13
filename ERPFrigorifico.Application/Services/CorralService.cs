using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Corrales;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.Enums;

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

            validarExistenciaAnimales(animales, animalIds);

            var animalesEnIngreso = await _movimientoAnimalRepository
                .GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.Ingreso);

            var animalYaEnCorral = await _movimientoAnimalRepository
                .GetAnimalesPorUltimoMovimiento(animalIds, TipoMovimiento.EntradaCorral);

            var animalesEnIngresoIds = animalesEnIngreso.Select(a => a.Id).ToList();

            //Verificamos si hay algun animal que no este en ingreso, si ya se movio a corral, tambien sirve como filtro.
            var hayInvalidos = animalIds.Any(id => !animalesEnIngresoIds.Contains(id));

            //Si hay algun animal que no este en ingreso, lanzamos una excepcion
            validarAnimalesEnIngreso(hayInvalidos);

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

        private void validarExistenciaAnimales(List<Animal> animales, List<int> animalIds)
        {
            if (animales.Count != animalIds.Count)
                throw new NotFoundException("Algunos animales no existen");
        }

        private void validarAnimalesEnIngreso(bool hayInvalidos)
        {
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en ingreso");
        }

    }
}