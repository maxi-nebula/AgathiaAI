using Kayal.Api.Models;

namespace Kayal.Api.Repositories;

public interface IJobRepository
{
    Task<Job?> FindMatchingJobAsync(
        string companyId,
        string? jobTitle);

    Task<Job> CreateAsync(Job job);

    Task UpdateAsync(Job job);
}