using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WoochiCode.Core.Llm.OpenAI;
using WoochiCode.Core.Vo;

namespace WoochiCode.zTest.Unit.Core.Llm.OpenAI;

public class OpenAIApiRequestBodyTest : IDisposable
{
    private readonly HttpClient _httpClient;

    public OpenAIApiRequestBodyTest()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://mookspace.mooo.com"),
        };
    }


    [Fact]
    public void serialize()
    {
        OpenAIApiRequestBody body = new OpenAIApiRequestBody()
        {
            Model = "local",
            Messages = new List<WoochiCode.Core.Llm.OpenAI.Message>()
            {
                new WoochiCode.Core.Llm.OpenAI.Message()
                {
                    Role = "user",
                    Content = "서울 날씨 알려줘."
                }
            },
            Temperature = 0.7f,
        };
        PrintRequestBody(body);
    }

    [Fact]
    public async Task DefaultPrompt()
    {
        OpenAIApiRequestBody body = new OpenAIApiRequestBody()
        {
            Model = "local",
            Messages = new List<WoochiCode.Core.Llm.OpenAI.Message>()
            {
                new WoochiCode.Core.Llm.OpenAI.Message()
                {
                    Role = "user",
                    Content = "안녕."
                }
            },
            Temperature = 0.7f,
        };
        string param = PrintRequestBody(body);

        LLMResponse result = await Send(param);

        Console.WriteLine(result.Choices[0].FinishReason);
        Console.WriteLine(result.Choices[0].Message.ToolCalls);
        Console.WriteLine(result.Choices[0].Message.Content);
    }

    [Fact]
    public async Task PromptWithJsonSchema()
    {
        ResponseFormat format = new();
        format.JsonSchema.Schema.Properties.Add("content", new PropertyType("string"));
        format.JsonSchema.Schema.Properties.Add("role", new PropertyType("string"));
        format.JsonSchema.Schema.Properties.Add("tool", new PropertyType("string"));

        format.JsonSchema.Schema.Required.Add("content");
        format.JsonSchema.Schema.Required.Add("role");
        format.JsonSchema.Schema.Required.Add("tool");

        OpenAIApiRequestBody body = new OpenAIApiRequestBody()
        {
            Model = "local",
            Messages = new List<WoochiCode.Core.Llm.OpenAI.Message>()
            {
                new WoochiCode.Core.Llm.OpenAI.Message()
                {
                    Role = "user",
                    Content = "안녕. 나좀 도와줘. 날씨는?"
                }
            },
            Temperature = 0.7f,
            ResponseFormat = format
        };
        string param = PrintRequestBody(body);

        LLMResponse result = await Send(param);

        Console.WriteLine(result.Choices[0].FinishReason);
        Console.WriteLine(result.Choices[0].Message.ToolCalls);
        Console.WriteLine(result.Choices[0].Message.Content);
    }

    [Fact]
    public async Task PromptWithTool()
    {
        OpenAIApiRequestBody body = new OpenAIApiRequestBody()
        {
            Model = "local",
            Messages = new List<WoochiCode.Core.Llm.OpenAI.Message>()
            {
                new WoochiCode.Core.Llm.OpenAI.Message()
                {
                    Role = "system",
                    Content = "알 수 없는 정보는 web_search 도구를 활용해"
                },
                new WoochiCode.Core.Llm.OpenAI.Message()
                {
                    Role = "user",
                    Content = "안녕. 나좀 도와줘. 날씨는?"
                }
            },
            Temperature = 0.7f,
        };
        string param = PrintRequestBody(body);

        LLMResponse result = await Send(param);

        Console.WriteLine(result.Id);
        Console.WriteLine(result.Object);
        Console.WriteLine(result.Model);
        Console.WriteLine(result.Choices[0].FinishReason);
        Console.WriteLine(result.Choices[0].Message.ToolCalls);
        Console.WriteLine(result.Choices[0].Message.Content);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    private async Task<LLMResponse> Send(string param)
    {
        using var content = new StringContent(param, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("/v1/chat/completions", content);

        LLMResponse result = await response.Content.ReadFromJsonAsync<LLMResponse>(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        }) ?? throw new InvalidOperationException("LLM 응답이 비어있습니다.");

        return result;
    }

    private string PrintRequestBody(OpenAIApiRequestBody body)
    {
        string json = JsonSerializer.Serialize(body, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });
        Console.WriteLine(json);
        return json;
    }
}
