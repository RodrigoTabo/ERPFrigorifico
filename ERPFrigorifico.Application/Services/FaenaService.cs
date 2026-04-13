using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class FaenaService(IAnimalRepository animalRepository,
        IFaenaRepository faenaRepository,
        IMovimientoAnimalRepository movimientoAnimalRepository,
        IUnitOfWorkRepository unitOfWorkRepository) : IFaenaService
    {

        private readonly IFaenaRepository _faenaRepository = faenaRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task EnviarAnimalesAFaena(List<Animal> animalesEnCorral)
        {

            var faena = new Faena
            {
                FechaProduccion = DateTime.UtcNow
            };

            foreach (var animal in animalesEnCorral)
            {
                animal.Faena = faena;

                var movimiento = new MovimientoAnimal
                {
                    AnimalId = animal.Id,
                    Faena = faena,
                    TipoMovimiento = TipoMovimiento.Faena,
                    FechaMovimiento = DateTime.UtcNow
                };

                animal.MovimientosAnimal.Add(movimiento);
            }
            await _unitOfWorkRepository.SaveChangesAsync();
        }

        public async Task ProcesarFaena(int faenaId)
        {
            var existeFaena = await ValidarFaenaExistente(faenaId);

            ValidarFaenaProcesada(existeFaena);

            ValidarFaenaTieneAnimal(existeFaena);

            foreach (var animal in existeFaena.Animales)
            {

                var canal = new Canal
                {
                    AnimalId = animal.Id,
                    FaenaId = existeFaena.Id,
                    Peso = animal.PesoIngreso
                };

                existeFaena.Canales.Add(canal);

            }
            await _unitOfWorkRepository.SaveChangesAsync();
        }


        // Metodos Privados de validacion.

        private async Task<Faena> ValidarFaenaExistente(int faenaId)
        {
            var faena = await _faenaRepository.GetFaenaById(faenaId);

            if (faena == null)
                throw new NotFoundException("Faena no encontrada");

            return faena;
        }

        private void ValidarFaenaProcesada(Faena existeFaena)
        {
            if (existeFaena.Canales.Any())
                throw new ConflictException("La faena ya fue procesada");
        }

        private void ValidarFaenaTieneAnimal(Faena faena)
        {
            if (faena.Animales == null || !faena.Animales.Any())
                throw new ConflictException("La faena no tiene animales asociados");
        }


    }
}
