using Kayal.Api.Models;

namespace Kayal.Api.Services;

public interface IKayalService
{
    Task<ChatResponse> ChatAsync(ChatRequest request);

    Task<JobEmailAnalysis> AnalyzeJobEmailAsync(
        EmailAnalysisRequest request);
}