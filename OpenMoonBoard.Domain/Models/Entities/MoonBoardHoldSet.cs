using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class MoonBoardHoldSet
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public required string Name { get; set; }
    //Navigational Properties
    public List<MoonBoardHold>? MoonBoardHolds { get; set; }
}
