namespace WoochiCode.Core.Vo;

public record Message
{
    public string Role { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public string? ToolCallId { get; init; }

    public string? Name { get; init; }

    public List<ToolCall>? ToolCalls { get; init; }
}

