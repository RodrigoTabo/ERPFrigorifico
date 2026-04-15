using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Corrales
{
    public interface ICorralService
    {
        Task EnviarAnimalesACorral(List<int> animalIds);
    }
}
