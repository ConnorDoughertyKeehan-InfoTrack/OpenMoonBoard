using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;

namespace OpenMoonBoard.Infrastructure.Repositories;
public class SettersRepository(OpenMoonBoardContext dbContext) : ISettersRepository
{
    public async Task InsertSetters(List<Setter> setters)
    {
        var existingIdentifiers = await dbContext.Setters
            .Where(s => setters.Select(x => x.SetterIdentifier).Contains(s.SetterIdentifier))
            .Select(s => s.SetterIdentifier)
            .ToListAsync();

        var newSetters = setters
            .Where(s => !existingIdentifiers.Contains(s.SetterIdentifier))
            .ToList();

        if (newSetters.Any())
        {
            await dbContext.Setters.AddRangeAsync(newSetters);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Setter>> GetAllSetters()
    {
        var result = await dbContext.Setters.Where(x=> x.Synced ==  false).AsNoTracking().ToListAsync();

        return result;
    }

    public async Task SetSetterToSynced(int id)
    {
        var setter = await dbContext.Setters.SingleOrDefaultAsync(x => x.Id == id);
        if (setter == null) {
            return;
        }

        setter.Synced = true;
        await dbContext.SaveChangesAsync();
    }
}
