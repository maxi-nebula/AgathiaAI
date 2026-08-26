using Kayal.Api.Models;
using Kayal.Api.Repositories;

namespace Kayal.Api.Services;

public class JobProcessingService : IJobProcessingService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IJobRepository _jobRepository;

    public JobProcessingService(
        ICompanyRepository companyRepository,
        IJobRepository jobRepository)
    {
        _companyRepository = companyRepository;
        _jobRepository = jobRepository;
    }

    public async Task ProcessAsync(JobEmailAnalysis analysis)
    {
        if (!analysis.IsJobRelated)
        {
            return;
        }

         if (string.IsNullOrWhiteSpace(analysis.CompanyName))
    {
        return;
    }

    Company? company =
        await _companyRepository.FindByNameAsync(
            analysis.CompanyName);

    if (company == null)
    {
        company = new Company
        {
            Name = analysis.CompanyName
        };

        company =
            await _companyRepository.CreateAsync(company);
    }

    Job? existingJob =
    await _jobRepository.FindMatchingJobAsync(
        company.Id!,
        analysis.JobTitle);

if (existingJob == null)
{
    Job newJob = new()
    {
        CompanyId = company.Id,
        JobTitle = analysis.JobTitle,
        Location = analysis.Location,
        Salary = analysis.Salary,
        EmploymentType = analysis.EmploymentType,
        VisaInformation = analysis.VisaInformation,
        CurrentStatus = analysis.Status,
        CreatedDate = DateTime.UtcNow,
        LastUpdated = DateTime.UtcNow,
        Recruiter = new Recruiter
        {
            Name = analysis.RecruiterName,
            Email = analysis.RecruiterEmail,
            PhoneNumber = analysis.RecruiterPhoneNumber
        }
    };

    if (!string.IsNullOrWhiteSpace(analysis.EventType))
    {
        newJob.Events.Add(new JobEvent
        {
            EventType = analysis.EventType,
            Status = analysis.Status,
            EventDate = analysis.EventDate,
            Summary = analysis.Summary
        });
    }

    await _jobRepository.CreateAsync(newJob);

    return;
}
existingJob.CurrentStatus = analysis.Status;
existingJob.LastUpdated = DateTime.UtcNow;

if (!string.IsNullOrWhiteSpace(analysis.Location))
{
    existingJob.Location = analysis.Location;
}

if (!string.IsNullOrWhiteSpace(analysis.Salary))
{
    existingJob.Salary = analysis.Salary;
}

if (!string.IsNullOrWhiteSpace(analysis.EmploymentType))
{
    existingJob.EmploymentType = analysis.EmploymentType;
}

if (!string.IsNullOrWhiteSpace(analysis.VisaInformation))
{
    existingJob.VisaInformation = analysis.VisaInformation;
}

if (existingJob.Recruiter == null)
{
    existingJob.Recruiter = new Recruiter();
}

if (!string.IsNullOrWhiteSpace(analysis.RecruiterName))
{
    existingJob.Recruiter.Name = analysis.RecruiterName;
}

if (!string.IsNullOrWhiteSpace(analysis.RecruiterEmail))
{
    existingJob.Recruiter.Email = analysis.RecruiterEmail;
}

if (!string.IsNullOrWhiteSpace(analysis.RecruiterPhoneNumber))
{
    existingJob.Recruiter.PhoneNumber = analysis.RecruiterPhoneNumber;
}

if (!string.IsNullOrWhiteSpace(analysis.EventType))
{
    existingJob.Events.Add(new JobEvent
    {
        EventType = analysis.EventType,
        Status = analysis.Status,
        EventDate = analysis.EventDate,
        Summary = analysis.Summary
    });
}

await _jobRepository.UpdateAsync(existingJob);
    }
}