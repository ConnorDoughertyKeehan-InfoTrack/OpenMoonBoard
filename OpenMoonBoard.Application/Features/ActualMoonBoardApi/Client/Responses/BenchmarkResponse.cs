using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;

public class BenchmarkData
{
    public int ProblemId { get; set; }
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Grade { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public int Difference { get; set; }
}
