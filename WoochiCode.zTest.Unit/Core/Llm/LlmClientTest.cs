using System.Net;
using WoochiCode.Core;
using WoochiCode.Core.Llm;
using WoochiCode.Core.Vo;

namespace WoochiCode.zTest.Unit.Core.Llm;

public class LlmClientTest : IDisposable
{
    private readonly LlmConfig _config;
    private readonly HttpClient _httpClient;
    private readonly TestHttpMessageHandler _messageHandler;

    public LlmClientTest()
    {
        _config = new LlmConfig
        {
            BaseUrl = "https://mookspace.mooo.com",
            ModelName = "local",
            MaxTokens = 8192,
            Temperature = 0.7f
        };

        _messageHandler = new TestHttpMessageHandler();
        _httpClient = new HttpClient(_messageHandler)
        {
            BaseAddress = new Uri(_config.BaseUrl)
        };
    }

    [Fact]
    public async Task ChatAsync_WithMessagesOnly_ShouldReturnSuccessfulResponse()
    {
        // Arrange
        var messages = new List<WoochiCode.Core.Llm.OpenAI.Message>
        {
            new() { Role = "user", Content = "안녕 테스트로 보내봐. 답변해줘." }
        };


        var client = new LlmClient(_config);

        // Act
        LLMResponse result = await client.ChatAsync(messages, ct: new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Choices);
        Console.WriteLine(result.Choices[0].Message.Content);
    }

    [Fact]
    public async Task ChatAsync_WithCancellationToken_ShouldRespectCancellation()
    {
        // Arrange
        var messages = new List<WoochiCode.Core.Llm.OpenAI.Message>
        {
            new() { Role = "user", Content = "Hello" }
        };

        var cts = new CancellationTokenSource();
        cts.Cancel();

        var client = new LlmClient(_config);

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(() =>
            client.ChatAsync(messages, ct: cts.Token)
        );
    }

    [Fact]
    public async Task ChatAsync_WithMultipleMessages_ShouldHandleConversation()
    {
        // Arrange
        var messages = new List<WoochiCode.Core.Llm.OpenAI.Message>
        {
            new() { Role = "user", Content = "What is 2+2?" },
            new() { Role = "assistant", Content = "2+2=4" },
            new() { Role = "user", Content = "What is 3+3?" }
        };

        var expectedResponse = new LLMResponse
        {
            Id = "multi-msg-test",
            Choices = new List<LLMChoice>
            {
                new()
                {
                    Message = new LLMMessage
                    {
                        Role = "assistant",
                        Content = "3+3=6"
                    },
                    FinishReason = "stop"
                }
            }
        };

        _messageHandler.SetResponse(expectedResponse);

        var client = new LlmClient(_config);

        // Act
        var result = await client.ChatAsync(messages, ct: new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal("3+3=6", result.Choices[0].Message.Content);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
        _messageHandler?.Dispose();
    }
}

/// <summary>
/// Helper class to mock HttpMessageHandler for testing
/// </summary>
internal class TestHttpMessageHandler : HttpMessageHandler
{
    private HttpResponseMessage? _responseMessage;
    private HttpStatusCode _statusCode = HttpStatusCode.OK;

    public void SetResponse(LLMResponse response)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(response, AppConstracts.JsonOpts);
        _responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
    }

    public void SetStatusCode(HttpStatusCode statusCode)
    {
        _statusCode = statusCode;
        _responseMessage = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent("Error", System.Text.Encoding.UTF8, "application/json")
        };
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_responseMessage == null)
        {
            throw new InvalidOperationException("Response not configured");
        }

        return Task.FromResult(_responseMessage);
    }
}
