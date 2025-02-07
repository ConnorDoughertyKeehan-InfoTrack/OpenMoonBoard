using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;

namespace OpenMoonBoard.Infrastructure.Repositories;
public class MoonBoardHoldsRepository(OpenMoonBoardContext dbContext) : IMoonBoardHoldsRepository
{
    public async Task<MoonBoardHold?> GetMoonBoardHoldByPositionAndBoardId(string position)
    {
        var result = await dbContext.MoonBoardHolds.Where(x => x.Position == position).SingleOrDefaultAsync();

        return result;
    }
}
