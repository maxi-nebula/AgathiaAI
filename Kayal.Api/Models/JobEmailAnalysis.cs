namespace Kayal.Api.Models
{
    public class JobEmailAnalysis
    {
        public bool IsJobRelated { get; set; }

        public string? CompanyName { get; set; }

        public string? JobTitle { get; set; }

        public string? RecruiterName { get; set; }

        public string? RecruiterEmail { get; set; }

        public string? RecruiterPhoneNumber{get;set;}

        public string? Location { get; set; }

        public string? Salary { get; set; }

        public string? EmploymentType { get; set; }

        public string? VisaInformation { get; set; }

        public string? EventType { get; set; }

        public string? Status { get; set; }

        public DateTime? EventDate { get; set; }

        public string? Summary { get; set; }
    }
}