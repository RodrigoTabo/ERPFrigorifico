using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.Faenas;


namespace ERPFrigorifico.Application.Interfaces.Faenas
{
    public interface IFaenaService
    {
        Task EnviarAnimalesAFaena(List<int> animalIds);
        Task<List<FaenaResponse>> GetAllFaenas();
        Task<FaenaResponse?> GetFaenaEnProceso();
        Task ProcesarFaena(int faenaId);
        Task TerminarFaena(int faenaId);
        

    }
}
