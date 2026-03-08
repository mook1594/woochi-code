namespace WoochiCode.Core.Vo;

public record LLMResponse
{
    public string Id { get; init; } = string.Empty;

    public string Object { get; init; } = string.Empty;

    public string Model { get; init; } = string.Empty;

    public List<LLMChoice> Choices { get; init; } = [];

    public LLMUsage? Usage { get; init; }
}

public record LLMChoice
{
    public LLMMessage Message { get; init; } = new();

    public string FinishReason { get; init; } = string.Empty;
}

public record LLMMessage
{
    public string Role { get; init; } = string.Empty;

    public string? Content { get; init; }

    public List<ToolCall>? ToolCalls { get; init; }
}

public record LLMUsage
{
    public int PromptTokens { get; init; }

    public int CompletionTokens { get; init; }

    public int TotalTokens { get; init; }
}

