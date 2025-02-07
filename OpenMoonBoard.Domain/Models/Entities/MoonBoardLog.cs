using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class MoonBoardLog
{
    public int Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public int RouteId { get; set; }
    public int LoginId { get; set; }
    public string? BetaVideoUrl { get; set; }
    public string? Attempts {  get; set; }
}
