using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces;

public interface IBoardsRepository
{
    Task<Board?> GetBoardByName(string name);
}