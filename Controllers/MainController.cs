using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private readonly IGPTService _GPTService;

    public MainController
    (
        IGPTService GPTService
    )
    {
        _GPTService = GPTService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(MessageDTO requestBody)
    {
        if (String.IsNullOrEmpty(requestBody.content)) return BadRequest("Message Content required");

        var responseBody = await _GPTService.GetResponseAsync(requestBody.content);
        if (responseBody == null) return BadRequest("GPT response failed");

        return Ok(responseBody);
    }

    [HttpPost("conversation")]
    public async Task<IActionResult> PostAsync(ConversationDTO requestBody)
    {
        if (requestBody.messages == null || !requestBody.messages.Any()) return BadRequest("Messages collection required");

        var responseBody = await _GPTService.GetResponseAsync(requestBody);
        if (responseBody == null) return BadRequest("GPT response failed");

        return Ok(responseBody);
    }
}