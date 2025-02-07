using MediatR;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Queries.GetBenchMarks;
public class GetBenchMarksQuery : IRequest<List<BenchmarkData>>
{
    public required MoonBoardCredentials Credentials { get; set; }
}
