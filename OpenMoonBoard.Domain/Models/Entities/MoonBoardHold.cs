using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class MoonBoardHold
{
    public int Id { get; set; }
    public int MoonBoardHoldSetId { get; set; }
    public required string Position { get; set; }

    //Navigational Properties
    public MoonBoardHoldSet? MoonBoardHoldSet { get; set; }
}
