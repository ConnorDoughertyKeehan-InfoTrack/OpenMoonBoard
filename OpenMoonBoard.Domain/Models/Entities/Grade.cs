using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class Grade
{
    public int Id { get; set; }
    public required string VGrade { get; set; }
    public required string FontGrade { get; set; }
}
