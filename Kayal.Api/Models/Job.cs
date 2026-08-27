namespace Kayal.Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Job
{
   [BsonRepresentation(BsonType.ObjectId)]
    public string? CompanyId { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? JobTitle { get; set; }

    public string? Location { get; set; }

    public string? Salary { get; set; }

    public string? EmploymentType { get; set; }

    public string? VisaInformation { get; set; }

    public string? CurrentStatus { get; set; }

    public DateTime? AppliedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastUpdated { get; set; }

    public Recruiter? Recruiter { get; set; }

    public List<JobEvent> Events { get; set; } = new();
}