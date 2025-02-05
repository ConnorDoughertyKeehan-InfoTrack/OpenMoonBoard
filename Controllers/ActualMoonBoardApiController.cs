using Microsoft.AspNetCore.Mvc;
using OpenMoonBoard.Features.ActualMoonBoardApi.Client;
using OpenMoonBoard.Features.ActualMoonBoardApi.Requests;

namespace OpenMoonBoard.Controllers;

[ApiController]
[Route("[controller]")]
public class ActualMoonBoardApiController(IMoonBoardClient moonBoardClient) : ControllerBase
{
    [HttpPost("GetBenchmarks")]
    public async Task<ActionResult<List<BenchmarkData>>> GetBenchmarks([FromBody] GetBenchmarksRequest request)
    {
        var result = await moonBoardClient.GetBenchmarks(request.username, request.password);
        return Ok(result);
    }
}
