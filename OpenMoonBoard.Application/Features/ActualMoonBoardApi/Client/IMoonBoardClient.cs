using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
public interface IMoonBoardClient
{
    Task<List<Problem>> GetAllClimbsBySetterIds(MoonBoardCredentials credentials, List<string> setterIds);
    Task<List<SetterResponse>> GetAllSettersFromUserLogBook(MoonBoardCredentials credentials, string userIdentifier);
    Task<List<BenchmarkData>> GetBenchmarks(MoonBoardCredentials credentials);
}