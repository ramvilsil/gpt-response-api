namespace Api.Services;

public interface IChatGPTService
{
    Task<string> GetResponseAsync(string message);
}