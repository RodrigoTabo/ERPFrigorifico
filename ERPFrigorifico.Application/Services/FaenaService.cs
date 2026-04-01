using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class FaenaService(IAnimalRepository animalRepository,
        IFaenaRepository faenaRepository,
        IMovimientoAnimalRepository movimientoAnimalRepository,
        IUnitOfWorkRepository unitOfWorkRepository) : IFaenaService
    {

        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IFaenaRepository _faenaRepository = faenaRepository;
        private readonly IMovimientoAnimalRepository _movimientoAnimalRepository = movimientoAnimalRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task EnviarAnimalesAFaena(List<int> animalIds)
        {
            var animales = await _animalRepository.GetByIds(animalIds);

            if (animales.Count != animalIds.Count)
                throw new NotFoundException("Algunos animales no existen");

            var animalesEnCorral = await _movimientoAnimalRepository.GetAnimalesPorUltimoMovimiento(TipoMovimiento.EntradaCorral);

            var animalesEnCorralesIds = animalesEnCorral.Select(a => a.Id).ToList();

            var hayInvalidos = animalIds.Any(id => !animalesEnCorralesIds.Contains(id));

            //Si hay algun animal que no este en corral, lanzamos una excepcion
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en corral");

            var faena = new Faena
            {
                FechaProduccion = DateTime.UtcNow
            };

            foreach (var animal in animales)
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

            if (existeFaena.Canales.Any())
                throw new ConflictException("La faena ya fue procesada");

            await ValidarFaenaTieneAnimal(existeFaena);

            foreach (var animal in existeFaena.Animales)
            {

                var canalIzq = new Canal
                {
                    AnimalId = animal.Id,
                    FaenaId = existeFaena.Id
                };
                var canalDer = new Canal
                {
                    AnimalId = animal.Id,
                    FaenaId = existeFaena.Id
                };

                existeFaena.Canales.Add(canalIzq);
                existeFaena.Canales.Add(canalDer);

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

        private async Task ValidarFaenaTieneAnimal(Faena faena)
        {
            if (faena.Animales == null || !faena.Animales.Any())
                throw new ConflictException("La faena no tiene animales asociados");
        }
    }
}
