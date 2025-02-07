using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Domain.Models.Entities;
public class MoonBoardRoute
{
    public int Id { get; set; }
    public Guid? ActualMoonBoardId {  get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public string? Setter {  get; set; }
    public int GradeId { get; set; }
    public int BoardId { get; set; }
    public required string Name { get; set; }
    //Navigational Properties
    public List<HoldsUsed>? HoldsUsed { get; set; }
    public Grade? Grade { get; set; }
    public Board? Board { get; set; }
    public bool IsBenchmark { get; set; }
}
