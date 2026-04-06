using ERPFrigorifico.Domain.Entities;

namespace ERPFrigorifico.Application.Interfaces.Animales
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetByIds(List<int> animalId);
    }
}
