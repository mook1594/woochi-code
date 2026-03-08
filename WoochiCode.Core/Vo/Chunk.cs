namespace WoochiCode.Core.Vo;

// SSE chunk for streaming
public record ChatCompletionChunk
{
    public List<ChunkChoice> Choices { get; init; } = [];
}

public record ChunkChoice
{
    public ChunkDelta Delta { get; init; } = new();
    public string? FinishReason { get; init; }
}

public record ChunkDelta
{
    public string? Content { get; init; }
    public List<ChunkToolCall>? ToolCalls { get; init; }
}

public record ChunkToolCall
{
    public int Index { get; init; }
    public string? Id { get; init; }
    public ChunkToolCallFunction? Function { get; init; }
}

public record ChunkToolCallFunction
{
    public string? Name { get; init; }
    public string? Arguments { get; init; }
}


