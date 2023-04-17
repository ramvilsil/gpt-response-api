using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Api.Services;

public class ChatGPTService : IChatGPTService
{
    private readonly HttpClient _httpClient;
    private const string GptApiUrl = "https://api.openai.com/v1/completions";

    public ChatGPTService
    (
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory
    )
    {
        _httpClient = httpClientFactory.CreateClient();
        ConfigureHttpClient(configuration);
    }

    public async Task<string> GetResponseAsync(string message)
    {
        var requestBody = CreateRequestBody(message);

        using var response = await _httpClient.PostAsync(GptApiUrl, new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();

        var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
        string responseText = Convert.ToString(result.choices[0].text);

        return responseText.Replace(message, "").Trim();
    }

    private void ConfigureHttpClient(IConfiguration configuration)
    {
        var apiKey = configuration.GetValue<string>("OpenAI:SecretAPIKey");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private static object CreateRequestBody(string message)
    {
        return new
        {
            model = "text-davinci-001",
            prompt = message,
            max_tokens = 15,
            temperature = 0
        };
    }
}