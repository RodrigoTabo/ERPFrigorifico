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

        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly IAnimalRepository _animalRepository = animalRepository;

        public async Task EnviarAnimalesACorral(List<int> animalesEnIngreso)
        {

            var animalIds = await _animalRepository.GetByIds(animalesEnIngreso);
            var animalesEnIngresoIds = animalIds.Select(a => a.Id).ToList();
            var hayInvalidosEnIngreso = animalesEnIngreso.Any(id => !animalesEnIngresoIds.Contains(id));

            validarAnimalesEnIngreso(hayInvalidosEnIngreso);

            foreach (var animal in animalIds)
            {
                var movimiento = new MovimientoAnimal
                {
                    AnimalId = animal.Id,
                    CorralId = 1,
                    TipoMovimiento = TipoMovimiento.EntradaCorral,
                    FechaMovimiento = DateTime.UtcNow
                };

                animal.MovimientosAnimal.Add(movimiento);
            }

            await _unitOfWorkRepository.SaveChangesAsync();

        }

        //Metodos privados
        private void validarAnimalesEnIngreso(bool hayInvalidos)
        {
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en ingreso");
        }
    }
}