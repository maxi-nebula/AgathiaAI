namespace Kayal.Api.Models
{
    public class EmailAnalysisRequest
    {
        public string? From { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }
    }
}