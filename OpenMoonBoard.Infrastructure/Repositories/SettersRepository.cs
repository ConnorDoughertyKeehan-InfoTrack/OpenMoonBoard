using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;

namespace OpenMoonBoard.Infrastructure.Repositories;
public class SettersRepository(OpenMoonBoardContext dbContext) : ISettersRepository
{
    public async Task InsertSetters(List<Setter> setters)
    {
        var alreadyInsertedSetterIdentifiers = await dbContext.Setters.Select(x => x.SetterIdentifier).ToListAsync();
        foreach (var setter in setters)
        {
            var alreadyInserted = alreadyInsertedSetterIdentifiers.Contains(setter.SetterIdentifier);
            //Ignore rows that have already been inserted
            if (alreadyInserted) continue;

            await dbContext.Setters.AddAsync(setter);
            alreadyInsertedSetterIdentifiers.Add(setter.SetterIdentifier);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Setter>> GetAllUnsyncedSetters()
    {
        var result = await dbContext.Setters.Where(x=> x.Synced == false || x.IncompleteSync == true).AsNoTracking().ToListAsync();

        return result;
    }

    public async Task SetSetterToSynced(int id, bool failed)
    {
        var setter = await dbContext.Setters.SingleOrDefaultAsync(x => x.Id == id);
        if (setter == null) {
            return;
        }

        setter.Synced = true;
        setter.IncompleteSync = failed;
        await dbContext.SaveChangesAsync();
    }
}
