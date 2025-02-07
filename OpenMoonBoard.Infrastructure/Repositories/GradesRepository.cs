using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using OpenMoonBoard.Infrastructure.Contexts;

namespace OpenMoonBoard.Infrastructure.Repositories;
public class GradesRepository(OpenMoonBoardContext dbContext) : IGradesRepository
{
    public async Task<Grade?> GetGradeByName(string name)
    {
        //Check if it's a V Grade
        if (name.ToLower().StartsWith("v"))
        {
            return await dbContext.Grades.Where(x => x.VGrade == name).SingleAsync();
        }

        return await dbContext.Grades.Where(x => x.FontGrade == name).SingleAsync();
    }
}
