using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces;
public interface ISettersRepository
{
    Task<List<Setter>> GetAllUnsyncedSetters();
    Task InsertSetters(List<Setter> setters);
    Task SetSetterToSynced(int id, bool failed);
}