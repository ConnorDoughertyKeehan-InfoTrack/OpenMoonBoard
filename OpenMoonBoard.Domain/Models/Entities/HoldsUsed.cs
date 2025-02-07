using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class HoldsUsed
{
    public int Id { get; set; }
    public int MoonBoardRouteId { get; set; }
    public int MoonBoardHoldId { get; set; }
    public bool IsStartHold { get; set; }
    public bool isEndHold { get; set; }
    //Navigational Properties
    public MoonBoardHold? MoonBoardHold { get; set; }
}
