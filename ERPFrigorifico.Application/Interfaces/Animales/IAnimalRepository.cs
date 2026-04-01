using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Animales
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetByIds(List<int> animalId);
    }
}
