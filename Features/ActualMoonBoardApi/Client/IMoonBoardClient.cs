namespace OpenMoonBoard.Features.ActualMoonBoardApi.Client;
public interface IMoonBoardClient
{
    Task<List<BenchmarkData>> GetBenchmarks(string username, string password);
}