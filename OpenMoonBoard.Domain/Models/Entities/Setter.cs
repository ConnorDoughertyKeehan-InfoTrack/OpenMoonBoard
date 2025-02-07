using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class Setter
{
    public int Id { get; set; }
    public required string SetterIdentifier { get; set; }
    public string? Name { get; set; }
    public bool Synced { get; set; }
}
