using Kayal.Api.Models;
using Kayal.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kayal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KayalController : ControllerBase
{
    private readonly IKayalService _kayalService;

    public KayalController(IKayalService kayalService)
    {
        _kayalService = kayalService;
    }

  [HttpPost("chat")]
public async Task<ActionResult<ChatResponse>> Chat(ChatRequest request)
{
    var response = await _kayalService.ChatAsync(request);

    return Ok(response);
}
}