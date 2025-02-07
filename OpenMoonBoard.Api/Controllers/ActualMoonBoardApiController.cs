using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenMoonBoard.Api.Controllers.Base;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Commands.SyncAllClimbsFromSettersInDatabase;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Commands.SyncRavioliBicepsAndHoseokLeesLogBookSetters;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Queries.GetBenchMarks;

namespace OpenMoonBoard.Api.Controllers;

public class ActualMoonBoardApiController(IMediator mediator) : BaseOpenMoonBoardController
{
    [HttpPost("GetBenchmarks")]
    public async Task<ActionResult<List<BenchmarkData>>> GetBenchmarks([FromBody] MoonBoardCredentials credentials)
    {
        var result = await mediator.Send(new GetBenchMarksQuery { Credentials = credentials });
        return Ok(result);
    }

    [HttpPost("SyncRavioliBicepsAndHoseokLeesLogBookSetters")]
    public async Task<IActionResult> SyncRBicepsAndHLeesLogBookSetters([FromBody] MoonBoardCredentials credentials)
    {
        await mediator.Send(new SyncRBicepsAndHLeesLogBookSettersCommand { Credentials = credentials });

        return NoContent();
    }

    [HttpPost("SyncAllClimbsFromSettersInDatabase")]
    public async Task<IActionResult> SyncAllClimbsFromSettersInDatabase([FromBody] MoonBoardCredentials credentials)
    {
        await mediator.Send(new SyncAllClimbsFromSettersInDatabaseCommand { Credentials = credentials });

        return NoContent();
    }
}
