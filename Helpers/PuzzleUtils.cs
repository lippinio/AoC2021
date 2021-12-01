using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class PuzzleUtils
{
    private readonly HttpClient _httpClient;
    private static string _baseUrl = "https://adventofcode.com";
    private static string? _sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSIONCOOKIE");
    private readonly Uri puzzleUri;

    public PuzzleUtils(int year, int day, HttpClient httpClient)
    {
        _httpClient = httpClient;
        this.puzzleUri = new Uri($@"{_baseUrl}/{year}/day/{day}");
    }

    public async Task<int[]> LoadIntArrayInput()
    {
        HttpResponseMessage response;
        
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, string.Concat(puzzleUri,"/input")))
        {
            requestMessage.Headers.Add("Cookie",string.Concat("session=",_sessionCookie));
            response = await _httpClient.SendAsync(requestMessage);
        }

        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadAsStringAsync();
        
        return Array.ConvertAll(responseBody.Split('\n').Where(s => s != "").ToArray(), int.Parse);
    }

    public async Task<bool> PostResult(int level, int answer)
    {
        HttpResponseMessage response;
        
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("level", level.ToString()),
            new KeyValuePair<string, string>("answer", answer.ToString())
        });
        
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, string.Concat(puzzleUri,"/answer")))
        {
            requestMessage.Content = content;
            requestMessage.Headers.Add("Cookie",string.Concat("session=",_sessionCookie));
            response = await _httpClient.SendAsync(requestMessage);
        }
        
        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        return responseBody.Contains("That's the right answer!");
    }
}
