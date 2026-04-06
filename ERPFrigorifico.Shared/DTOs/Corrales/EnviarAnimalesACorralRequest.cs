using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Corral
{
    public record EnviarAnimalesACorralRequest(List<int> animalIds, int corralId);
}
