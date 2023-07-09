namespace Application.DTOs;

public class ConversationDTO
{
    public string? system { get; set; }
    public List<MessageDTO>? messages { get; set; }
}