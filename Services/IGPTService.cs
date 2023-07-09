using Application.DTOs;

namespace Application.Services;

public interface IGPTService
{
    public Task<MessageDTO?> GetResponseAsync(string prompt);
    public Task<MessageDTO?> GetResponseAsync(ConversationDTO conversation);
}