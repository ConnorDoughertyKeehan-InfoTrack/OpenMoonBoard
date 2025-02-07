using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Domain.Interfaces
{
    public interface IMoonBoardHoldsRepository
    {
        Task<MoonBoardHold?> GetMoonBoardHoldByPositionAndBoardId(string position);
    }
}