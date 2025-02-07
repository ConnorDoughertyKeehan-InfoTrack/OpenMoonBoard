using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;


namespace OpenMoonBoard.Infrastructure.Repositories;
public class BoardsRepository(OpenMoonBoardContext dbContext) : IBoardsRepository
{
    public async Task<Board?> GetBoardByName(string name)
    {
        var result = await dbContext.Boards
            .AsNoTracking()
            .Include(x => x.MoonBoardHoldSets!)
                .ThenInclude(x => x.MoonBoardHolds)
            .Where(x => x.Name == name)
            .SingleOrDefaultAsync();

        return result;
    }
}
