using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.Faenas;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class FaenaService(IAnimalRepository animalRepository,
        IFaenaRepository faenaRepository,
        IMovimientoAnimalRepository movimientoAnimalRepository,
        IUnitOfWorkRepository unitOfWorkRepository) : IFaenaService
    {

        private readonly IFaenaRepository _faenaRepository = faenaRepository;
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;


        //Aca antes de enviar los animales a la faena, necesito ver si hay una faena en modo pendiente o en curso para asignarles animales a la que este en curso por el amor de dios
        public async Task EnviarAnimalesAFaena(List<int> animalesEnCorral)
        {

            var animalIds = await _animalRepository.GetByIds(animalesEnCorral);

            var animalesEnCorralesIds = animalIds.Select(a => a.Id).ToList();

            var hayInvalidosEnFaena = animalesEnCorral.Any(id => !animalesEnCorralesIds.Contains(id));

            ValidarAnimalesEnCorral(hayInvalidosEnFaena);

            var faenaActiva = await FaenaActiva();

            AgregarAnimalesAFaena(animalIds, faenaActiva);

            await _unitOfWorkRepository.SaveChangesAsync();
        }

        public async Task<List<FaenaResponse>> GetAllFaenas()
            => await _faenaRepository.GetAllFaenas();

        public async Task<FaenaResponse?> GetFaenaEnProceso()
            => await _faenaRepository.GetFaenaEnProceso();

        public async Task ProcesarFaena(int faenaId)
        {
            var faena = await ValidarFaenaExistente(faenaId);

            ValidarFaenaProcesada(faena);

            ValidarFaenaTieneAnimal(faena);

            await CambiarEstadoFaena(faena);

            await _unitOfWorkRepository.SaveChangesAsync();
        }

        public async Task TerminarFaena(int faenaId)
        {
            var faena = await ValidarFaenaExistente(faenaId);

            ValidarFaenaProcesada(faena);

            ValidarFaenaTieneAnimal(faena);

            await CambiarEstadoFaena(faena);

            await _unitOfWorkRepository.SaveChangesAsync();
        }

        public async Task CambiarEstadoFaena(Faena faena)
        {
            if (faena.TipoFaena == TipoFaena.Pendiente)
                faena.TipoFaena = TipoFaena.EnCurso;
            else if (faena.TipoFaena == TipoFaena.EnCurso)
                faena.TipoFaena = TipoFaena.Procesada;
        }

        // Metodos Privados de validacion.


        private void AgregarAnimalesAFaena(List<Animal> animales, Faena faena)
        {
            foreach (var animal in animales)
            {
                animal.Faena = faena;

                animal.MovimientosAnimal ??= new List<MovimientoAnimal>();

                animal.MovimientosAnimal.Add(new MovimientoAnimal
                {
                    AnimalId = animal.Id,
                    Faena = faena,
                    TipoMovimiento = TipoMovimiento.Faena,
                    FechaMovimiento = DateTime.UtcNow
                });
            }
        }

        private async Task<Faena> FaenaActiva()
            => await _faenaRepository.FaenaActiva();

        private void ValidarAnimalesEnCorral(bool hayInvalidos)
        {
            if (hayInvalidos)
                throw new ConflictException("Algunos animales no están en corral");
        }

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
