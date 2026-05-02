using Microsoft.Extensions.Configuration;
using mktsystem.application.Interfaces;
using System.Text;
using System.Text.Json;

namespace mktsystem.infrastructure.Services
{
    public class GeminiService(IConfiguration config,
        HttpClient httpClient) : IAIService
    {
        private readonly string apiKey = config["GeminiApi:ApiKey"];

        public async Task<string> GenerateAsync(string prompt)
        {
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent";

            var body = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(body);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            // ✅ IMPORTANT: API KEY HEADER
            request.Headers.Add("X-goog-api-key", apiKey);

            var response = await httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API failed: {result}");
            }

            using var doc = JsonDocument.Parse(result);

            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";
        }
    }
}
