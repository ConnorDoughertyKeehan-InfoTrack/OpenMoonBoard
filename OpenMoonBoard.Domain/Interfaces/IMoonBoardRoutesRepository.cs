using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces;

public interface IMoonBoardRoutesRepository
{
    Task AddRoutes(List<MoonBoardRoute> moonBoardRoutes);
}