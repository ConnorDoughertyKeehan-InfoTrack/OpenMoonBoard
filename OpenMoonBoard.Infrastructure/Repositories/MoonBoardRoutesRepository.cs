using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;


namespace OpenMoonBoard.Infrastructure.Repositories;
public class MoonBoardRoutesRepository(OpenMoonBoardContext dbContext) : IMoonBoardRoutesRepository
{
    public async Task AddRoutes(List<MoonBoardRoute> moonBoardRoutes)
    {
        foreach(var  moonBoardRoute in moonBoardRoutes)
        {
            var alreadyInserted = await IsRouteAlreadyInserted(moonBoardRoute.Name);
            //Ignore rows that have already been inserted
            if (alreadyInserted) continue;

            await dbContext.MoonBoardRoutes.AddAsync(moonBoardRoute);
        }

        dbContext.SaveChanges();
    }
    private async Task<bool> IsRouteAlreadyInserted(string name)
    {
        var result = await dbContext.MoonBoardRoutes
            .AsNoTracking()
            .AnyAsync(x => x.Name == name);

        return result;
    }
}
