using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;


namespace OpenMoonBoard.Infrastructure.Repositories;
public class MoonBoardRoutesRepository(OpenMoonBoardContext dbContext) : IMoonBoardRoutesRepository
{
    public async Task AddRoutes(List<MoonBoardRoute> moonBoardRoutes)
    {
        var alreadyInsertedRouteNames = await dbContext.MoonBoardRoutes.Select(x => x.Name).ToListAsync();
        foreach (var  moonBoardRoute in moonBoardRoutes)
        {
            var alreadyInserted = alreadyInsertedRouteNames.Contains(moonBoardRoute.Name);
            //Ignore rows that have already been inserted
            if (alreadyInserted) continue;

            await dbContext.MoonBoardRoutes.AddAsync(moonBoardRoute);
            alreadyInsertedRouteNames.Add(moonBoardRoute.Name);
        }

        dbContext.SaveChanges();
    }
}
