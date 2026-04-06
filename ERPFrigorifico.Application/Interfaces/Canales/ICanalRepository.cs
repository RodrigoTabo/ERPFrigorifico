using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Canales
{
    public interface ICanalRepository
    {
        Task <Canal> GetCanalById(int CanalId);
    }
}
