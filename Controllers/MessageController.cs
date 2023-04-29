using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.DTOs;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IChatGPTService _chatGPTService;

    public MessageController
    (
        IChatGPTService chatGPTService
    )
    {
        _chatGPTService = chatGPTService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string message)
    {
        string response = await _chatGPTService.GetResponseAsync(message);
        Console.Write($"ChatGPT Response (Latin Script): {response}");
        return Ok(new { response });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageRequest request)
    {
        string response = await _chatGPTService.GetResponseAsync(request.Message);
        Console.Write($"ChatGPT Response (Latin Script): {response}");
        return Ok(new { response });
    }
}