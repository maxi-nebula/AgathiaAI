namespace Kayal.Api.Models;

public class JobEvent
{
    public string? EventType { get; set; }

    public string? Status { get; set; }

    public DateTime? EventDate { get; set; }

    public string? Summary { get; set; }
}