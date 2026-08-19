using Kayal.Api.Models;
using Kayal.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kayal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailAnalysisController : ControllerBase
{
    private readonly IKayalService _kayalService;

    public EmailAnalysisController(IKayalService kayalService)
    {
        _kayalService = kayalService;
    }

    [HttpPost]
    public async Task<ActionResult<JobEmailAnalysis>> Analyze(
        EmailAnalysisRequest request)
    {
        JobEmailAnalysis result =
            await _kayalService.AnalyzeJobEmailAsync(request);

        return Ok(result);
    }
}