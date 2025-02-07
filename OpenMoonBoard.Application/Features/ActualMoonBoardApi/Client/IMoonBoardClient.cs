using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
public interface IMoonBoardClient
{
    Task<(List<Problem>, bool)> GetAllClimbsBySetterId(MoonBoardCredentials credentials, string setterId);
    Task<List<SetterResponse>> GetAllSettersFromUserLogBook(MoonBoardCredentials credentials, string userIdentifier);
    Task<List<BenchmarkData>> GetBenchmarks(MoonBoardCredentials credentials);
}