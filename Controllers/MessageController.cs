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
    public async Task<string> Get([FromQuery] string message)
    {
        string response = await _chatGPTService.GetResponseAsync(message);
        return response;
    }

    [HttpPost]
    public async Task<string> Post([FromBody] MessageRequest request)
    {
        string response = await _chatGPTService.GetResponseAsync(request.Message);
        return response;
    }
}