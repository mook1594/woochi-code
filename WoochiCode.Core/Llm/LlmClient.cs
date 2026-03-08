using System.Net.Http.Json;
using System.Text.Json;
using WoochiCode.Core.Llm.OpenAI;
using WoochiCode.Core.Logger;
using WoochiCode.Core.Vo;

namespace WoochiCode.Core.Llm;

public class LlmClient : IDisposable
{
    private readonly HttpClient _Http;
    private readonly LlmConfig _Config;

    public LlmClient(LlmConfig config)
    {
        _Config = config;
        _Http = new HttpClient
        {
            BaseAddress = new Uri(config.BaseUrl.TrimEnd('/')),
            Timeout = TimeSpan.FromMinutes(5),
        };
    }

    public async Task<LLMResponse> ChatAsync(
        List<OpenAI.Message> messages,
        List<ToolDefinition>? tools = null,
        CancellationToken ct = default
        )
    {
        OpenAIApiRequestBody body = BuildBody(messages, tools, stream: false);
        ConsoleLogger.Instance.Debug("openai body", body);

        HttpResponseMessage response = await _Http.PostAsJsonAsync("/v1/chat/completions", body,
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            }, ct);
        if (!response.IsSuccessStatusCode)
        {
            var text = await response.Content.ReadAsStringAsync(ct);
            ConsoleLogger.Instance.Error(LogCategory.Message, "API 에러", response.StatusCode);
            throw new InvalidOperationException($"LLM API Error {(int)response.StatusCode}");
        }

        var result = await response.Content.ReadFromJsonAsync<LLMResponse>(AppConstracts.JsonOpts, ct)
            ?? throw new InvalidOperationException("LLM 응답이 비어있습니다.");

        return result;
    }

    private OpenAIApiRequestBody BuildBody(List<OpenAI.Message> messages, List<ToolDefinition>? tools, bool stream)
    {
        if (tools is { Count: > 0 })
        {
            return new OpenAIApiRequestBody()
            {
                Model = _Config.ModelName,
                Messages = messages,
                MaxTokens = _Config.MaxTokens,
                Temperature = _Config.Temperature,
                Stream = stream,
                Tools = tools,
                ToolChoice = "auto"
            };
        }

        ResponseFormat format = new ResponseFormat();
        format.JsonSchema.Schema.Properties.Add("role", new PropertyType("string"));
        format.JsonSchema.Schema.Properties.Add("conetent", new PropertyType("string"));
        format.JsonSchema.Schema.Properties.Add("tool", new PropertyType("string"));

        return new OpenAIApiRequestBody
        {
            Model = _Config.ModelName,
            Messages = messages,
            //ResponseFormat = format,
            //MaxTokens = _Config.MaxTokens,
            //Temperature = _Config.Temperature,
            //Stream = stream
        };
    }

    public void Dispose() => _Http?.Dispose();
}
