using ERPFrigorifico.Domain.Entities;


namespace ERPFrigorifico.Application.Interfaces.Faenas
{
    public interface IFaenaService
    {
        Task EnviarAnimalesAFaena(List<Animal> animalIds);
        Task ProcesarFaena(int faenaId);
    }
}
