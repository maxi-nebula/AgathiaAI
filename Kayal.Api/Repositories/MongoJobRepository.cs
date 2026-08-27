using Kayal.Api.Models;
using MongoDB.Driver;

namespace Kayal.Api.Repositories;

public class MongoJobRepository : IJobRepository
{
    private readonly IMongoCollection<Job> _jobs;

    public MongoJobRepository(IMongoDatabase database)
    {
        _jobs = database.GetCollection<Job>("Jobs");
    }

  public async Task<Job?> FindMatchingJobAsync(
    string companyId,
    string? jobTitle)
{
    return await _jobs
        .Find(job =>
            job.CompanyId == companyId &&
            job.JobTitle == jobTitle)
        .FirstOrDefaultAsync();
}
  public async Task<Job> CreateAsync(Job job)
{
    await _jobs.InsertOneAsync(job);
    return job;
}

   public async Task UpdateAsync(Job job)
{
    await _jobs.ReplaceOneAsync(
        existingJob => existingJob.Id == job.Id,
        job);
}
}