using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;

public class MoonBoardCredentials 
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
