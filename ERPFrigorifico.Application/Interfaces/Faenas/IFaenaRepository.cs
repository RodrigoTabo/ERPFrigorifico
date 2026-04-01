using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Faenas
{
    public interface IFaenaRepository
    {
        Task<Faena> GetFaenaById(int faenaId);
    }
}
