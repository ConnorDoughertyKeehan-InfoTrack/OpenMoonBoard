using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;
using Newtonsoft.Json;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
public class MoonBoardClient : IMoonBoardClient
{
    private readonly HttpClient _httpClient;
    public MoonBoardClient()
    {
        var cookies = new CookieContainer();
        var handler = new HttpClientHandler
        {
            CookieContainer = cookies,
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.All
        };

        var httpClient = new HttpClient(handler);
        httpClient.Timeout = TimeSpan.FromMinutes(2);
        _httpClient = httpClient;
        
    }
    public async Task<List<SetterResponse>> GetAllSettersFromUserLogBook(MoonBoardCredentials credentials, string userIdentifier)
    {
        await LoginAsync(credentials.Username, credentials.Password);
        var url = $"https://www.moonboard.com/Account/GetLogbook/{userIdentifier}";

        var (logbookDays, _) = await genericMoonBoardRequest<LogbookDayId>(url);

        HashSet<SetterResponse> settersHash = new HashSet<SetterResponse>();

        //This gets everyday that I logged a climb
        foreach (var logbookDay in logbookDays)
        {
            var problemsRequestUrl = $"https://www.moonboard.com/Account/GetLogbookEntries/{userIdentifier}/{logbookDay.Id}";

            var (logbookDayResponses, _) = await genericMoonBoardRequest<ProblemData>(problemsRequestUrl);

            //This gets every climb logged in said day
            foreach (var climb in logbookDayResponses)
            {
                settersHash.Add(climb.Problem.Setter);
            }
        }

        var result = settersHash.ToList();
        return result;
    }

    public async Task<List<BenchmarkData>> GetBenchmarks(MoonBoardCredentials credentials)
    {
        await LoginAsync(credentials.Username, credentials.Password);
        var url = "https://www.moonboard.com/Dashboard/GetBenchmarks";
        var (result, _) = await genericMoonBoardRequest<BenchmarkData>(url);

        return result;
    }

    public async Task<(List<Problem>, bool)> GetAllClimbsBySetterId(MoonBoardCredentials credentials, string setterId)
    {
        await LoginAsync(credentials.Username, credentials.Password);

        var settersProblemsUrl = $"https://www.moonboard.com/Account/GetProblems/{setterId}";

        //This is the only endpoint that actually gives the moves on the climbs
        var (problems, failed) = await genericMoonBoardRequest<Problem>(settersProblemsUrl);

        return (problems, failed);
    }

    private async Task LoginAsync(string username, string password)
    {
        // 1) GET the login page to start session and capture cookies + hidden token
        var getLoginPageUrl = "https://www.moonboard.com/account/login";
        var getResponse = await _httpClient.GetAsync(getLoginPageUrl);
        getResponse.EnsureSuccessStatusCode();

        var loginPageHtml = await getResponse.Content.ReadAsStringAsync();

        // We'll do a simple Regex to pull out the hidden __RequestVerificationToken 
        // from the form <input> if present:
        var tokenMatch = Regex.Match(
            loginPageHtml,
            @"<input\s+name=""__RequestVerificationToken""\s+type=""hidden""\s+value=""([^""]+)"""
        );
        var requestVerificationToken = tokenMatch.Success
            ? tokenMatch.Groups[1].Value
            : throw new Exception("Could not find __RequestVerificationToken in login page HTML.");

        // 2) POST credentials with the verification token
        var postLoginUrl = "https://www.moonboard.com/account/login";

        // The form fields your cURL showed:
        var formFields = new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = requestVerificationToken,
            ["form_key"] = "nobUfmjm7O4jbcMa", // or whatever your form_key actually is
            ["Login.Username"] = username,
            ["Login.Password"] = password,
            ["send"] = "" // If the form field 'send=' is needed
        };

        using var content = new FormUrlEncodedContent(formFields);

        //This response actually sets the cookies in my cookie container so I now have an authed client.
        var postResponse = await _httpClient.PostAsync(postLoginUrl, content);
        postResponse.EnsureSuccessStatusCode();
    }

    //With default 2016 filter
    private async Task<(List<T>, bool)> genericMoonBoardRequest<T>(string url, string? filter = "setupId~eq~'1'~and~Configuration~eq~3")
    {
        List<T> result = [];
        bool failed = false;
        try
        {
            var pageLength = 20;
            int resultCount = pageLength;
            int pageNumber = 1;
            while (resultCount >= pageLength)
            {
                var formFields = new Dictionary<string, string>
                {
                    ["sort"] = "",
                    ["page"] = $"{pageNumber}",
                    ["pageSize"] = $"{pageLength}",
                    ["group"] = "",
                    ["filter"] = "setupId~eq~'1'~and~Configuration~eq~3"
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Content = new FormUrlEncodedContent(formFields);

                var response = await _httpClient.SendAsync(request);

                var responseJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseJson);
                using JsonDocument document = JsonDocument.Parse(responseJson);
                var jsonData = document.RootElement.GetProperty("Data").ToString();
                var convertedResponse = JsonConvert.DeserializeObject<List<T>>(jsonData)!;
                result.AddRange(convertedResponse);

                resultCount = convertedResponse.Count;
                pageNumber++;
            }
        }
        catch (Exception ex)
        {
            failed = true;
            Console.WriteLine(ex.ToString());
        }

        return (result, failed);
    }
}