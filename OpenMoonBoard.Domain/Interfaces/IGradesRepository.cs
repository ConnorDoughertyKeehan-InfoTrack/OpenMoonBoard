using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces;
public interface IGradesRepository
{
    Task<Grade?> GetGradeByName(string name);
}