using MediatR;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Queries.GetBenchMarks;
public class GetBenchMarksQueryHandler(IMoonBoardClient moonBoardClient) : IRequestHandler<GetBenchMarksQuery, List<BenchmarkData>>
{
    public async Task<List<BenchmarkData>> Handle(GetBenchMarksQuery query, CancellationToken cancellationToken)
    {
        var result = await moonBoardClient.GetBenchmarks(query.Credentials);

        return result;
    }
}
