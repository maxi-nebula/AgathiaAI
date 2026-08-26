using Kayal.Api.Models;

namespace Kayal.Api.Services;

public interface IJobProcessingService
{
    Task ProcessAsync(JobEmailAnalysis analysis);
}