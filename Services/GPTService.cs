using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Application.Configuration;
using Application.DTOs;

namespace Application.Services;

public class GPTService : IGPTService
{
    private readonly IOptions<OpenAiApiOptions> _openAiApiOptions;
    private readonly HttpClient _httpClient;

    public GPTService
    (
        IOptions<OpenAiApiOptions> openAiApiOptions,
        IHttpClientFactory httpClientFactory
    )
    {
        _openAiApiOptions = openAiApiOptions;
        _httpClient = httpClientFactory.CreateClient();
        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiOptions.Value.Key);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<MessageDTO?> GetResponseAsync(string prompt)
    {
        var requestBody = new StringContent(
            JsonConvert.SerializeObject(new
            {
                model = _openAiApiOptions.Value.DefaultChatModel,
                messages = new object[]
                {
                    new {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0
            }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_openAiApiOptions.Value.Url}chat/completions", requestBody);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return null;
        }

        var responseBody = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        string responseMessageContent;
        try
        {
            responseMessageContent = responseBody.choices[0].message.content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return null;
        }

        return new MessageDTO
        {
            role = "assistant",
            content = responseMessageContent
        };
    }

    public async Task<MessageDTO?> GetResponseAsync(ConversationDTO conversation)
    {
        if (!String.IsNullOrEmpty(conversation.system))
        {
            conversation.messages.Insert(0,
                new MessageDTO
                {
                    role = "system",
                    content = conversation.system
                }
            );
        }

        var requestBody = new StringContent(
            JsonConvert.SerializeObject(new
            {
                model = _openAiApiOptions.Value.DefaultChatModel,
                messages = conversation.messages,
                temperature = 0
            }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_openAiApiOptions.Value.Url}chat/completions", requestBody);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return null;
        }

        var responseBody = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        string responseMessageContent;
        try
        {
            responseMessageContent = responseBody.choices[0].message.content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return null;
        }

        return new MessageDTO
        {
            role = "assistant",
            content = responseMessageContent
        };
    }
}