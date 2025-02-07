using Microsoft.EntityFrameworkCore;
using OpenMoonBoard.Domain.Models.Entities;

namespace OpenMoonBoard.Infrastructure.Contexts;
public class OpenMoonBoardContext : DbContext
{
    public OpenMoonBoardContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<HoldsUsed> HoldsUsed { get; set; }
    public DbSet<MoonBoardHold> MoonBoardHolds { get; set; }
    public DbSet<MoonBoardLog> MoonBoardLogs { get; set; }
    public DbSet<MoonBoardRoute> MoonBoardRoutes { get; set; }
    public DbSet<Setter> Setters { get; set; }
}
