
#pragma warning disable OPENAI001
using Kayal.Api.Models;
using OpenAI.Responses;
using Kayal.Api.Prompts;
using System.Text.Json;

namespace Kayal.Api.Services;

public class KayalService : IKayalService
{
    private readonly ResponsesClient _responsesClient;
    private readonly string _deployment;

    public KayalService(
        ResponsesClient responsesClient,
        IConfiguration configuration)
    {
        _responsesClient = responsesClient;

        _deployment =
            configuration["AzureOpenAI:Deployment"]
            ?? throw new InvalidOperationException(
                "AzureOpenAI:Deployment is missing.");

               
    }

public async Task<ChatResponse> ChatAsync(ChatRequest request)
{
    CreateResponseOptions options = new()
    {
        Model = _deployment,
        Instructions=KayalPrompt.Instructions,

        InputItems =
        {
            ResponseItem.CreateUserMessageItem(request.Message)
        }
    };

    ResponseResult response =
        await _responsesClient.CreateResponseAsync(options);

    return new ChatResponse
    {
        Message = response.GetOutputText()
    };
}

public async Task<JobEmailAnalysis> AnalyzeJobEmailAsync(
    EmailAnalysisRequest request)
{
    string emailContent = $"""
        From: {request.From}
        Subject: {request.Subject}

        Body:
        {request.Body}
        """;

    CreateResponseOptions options = new()
    {
        Model = _deployment,
        Instructions = JobEmailAnalysisPrompt.Instructions,

        InputItems =
        {
            ResponseItem.CreateUserMessageItem(emailContent)
        }
    };

    ResponseResult response =
        await _responsesClient.CreateResponseAsync(options);

    string json = response.GetOutputText();
    Console.WriteLine("KAYAL RESPONSE:");
Console.WriteLine(json);

    JobEmailAnalysis? analysis =
        JsonSerializer.Deserialize<JobEmailAnalysis>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

    return analysis
        ?? throw new InvalidOperationException(
            "Kayal returned an invalid email analysis.");
}
}