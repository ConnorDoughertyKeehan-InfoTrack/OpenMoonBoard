using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenMoonBoard.Features.ActualMoonBoardApi.Client;
public class MoonBoardClient : IMoonBoardClient
{

    public async Task<List<BenchmarkData>> GetBenchmarks(string username, string password)
    {
        var cookies = new CookieContainer();
        var handler = new HttpClientHandler
        {
            CookieContainer = cookies,
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.All
        };

        var httpClient = new HttpClient(handler);
        await LoginAsync(httpClient, username, password);
        var result = await GetBenchmarksAsync(httpClient);

        return result;
    }

    private async Task LoginAsync(HttpClient httpClient, string username, string password)
    {
        // 1) GET the login page to start session and capture cookies + hidden token
        var getLoginPageUrl = "https://www.moonboard.com/account/login";
        var getResponse = await httpClient.GetAsync(getLoginPageUrl);
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

        var postResponse = await httpClient.PostAsync(postLoginUrl, content);
        postResponse.EnsureSuccessStatusCode();
    }

    private async Task<List<BenchmarkData>> GetBenchmarksAsync(HttpClient httpClient)
    {
        var url = "https://www.moonboard.com/Dashboard/GetBenchmarks";

        // The cURL uses "application/x-www-form-urlencoded" with these fields:
        

        var result = new List<BenchmarkData>();
        int pageNumber = 1;
        int resultCount = 999;
        var mb2016Filter = "setupId~eq~'1'~and~Configuration~eq~3";

        while (resultCount >= 40)
        {
            var formFields = new Dictionary<string, string>
            {
                ["sort"] = "",
                ["page"] = pageNumber.ToString(),
                ["pageSize"] = "40",
                ["group"] = "",
                ["aggregate"] = "Score-sum~MaxScore-sum",
                ["filter"] = mb2016Filter
            };

            using var content = new FormUrlEncodedContent(formFields);
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var decodedResponse = JsonSerializer.Deserialize<BenchmarksResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var decodedData = decodedResponse?.Data ?? [];
            resultCount = decodedData.Count;
            pageNumber++;

            result.AddRange(decodedData);

        }

        var consolePrint = result.Count;
        Console.WriteLine(consolePrint);
        return result;
    }
}

// Models for the JSON returned by GetBenchmarks
public class BenchmarksResponse
{
    public List<BenchmarkData> Data { get; set; }
}

public class BenchmarkData
{
    public int ProblemId { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Grade { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public int Difference { get; set; }
}
