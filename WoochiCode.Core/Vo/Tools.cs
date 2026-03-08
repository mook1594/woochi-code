namespace WoochiCode.Core.Vo;

public class ToolResult
{
    public bool Success { get; init; }
    public string Output { get; init; } = string.Empty;
    public string? FilePath { get; init; }
    public string? Error { get; init; }
}

public record ToolPropertyDef
{
    public string Type { get; init; } = "string";
    public string Description { get; init; } = string.Empty;
}

public record ToolParameters
{
    public string Type { get; init; } = "object";
    public Dictionary<string, ToolPropertyDef> Properties { get; init; } = [];
    public List<string> Required { get; init; } = [];
}

public record ToolFunction
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ToolParameters Parameters { get; init; } = new();
}

public record ToolDefinition
{
    public string Type { get; init; } = "function";
    public ToolFunction Function { get; init; } = new();
}

public record ToolCall
{
    public string Id { get; init; } = string.Empty;

    public string Type { get; init; } = "function";

    public ToolCallFunction Function { get; init; } = new();
}

public record ToolCallFunction
{
    public string Name { get; init; } = string.Empty;

    public string Arguments { get; init; } = "{}";
}
