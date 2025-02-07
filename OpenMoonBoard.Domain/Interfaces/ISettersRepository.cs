using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces;
public interface ISettersRepository
{
    Task<List<Setter>> GetAllSetters();
    Task InsertSetters(List<Setter> setters);
    Task SetSetterToSynced(int id);
}