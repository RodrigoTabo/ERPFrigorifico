using ERPFrigorifico.Domain.Entities;


namespace ERPFrigorifico.Application.Interfaces.Faenas
{
    public interface IFaenaService
    {
        Task EnviarAnimalesAFaena(List<int> animalIds);
        Task ProcesarFaena(int faenaId);
    }
}
