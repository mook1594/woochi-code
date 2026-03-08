using WoochiCode.Core.Utils;

namespace WoochiCode.Core.Vo;

public record AppConfig
{
    public LlmConfig Llm { get; init; } = new();
    public AgentConfig Agent { get; init; } = new();
    public SkillsConfig Skills { get; init; } = new();
}

public record LlmConfig
{
    public string BaseUrl { get; init; } = "http://localhost:8080";
    public string ModelName { get; init; } = "local-model";
    public int MaxTokens { get; init; } = 4096;
    public float Temperature { get; init; } = 0.2f;
    public int ContextWindow { get; init; } = 8192;
}

public record AgentConfig
{
    public int MaxIterations { get; init; } = 20;
    public bool ConfirmWrite { get; init; } = true;
    public bool ConfirmBash { get; init; } = true;
    public List<string> AllowedPaths { get; init; } = [];
}

public record SkillsConfig
{
    public string GlobalDir = PathUtils.GetGlobalConfigDirAbsolutePath(AppConstracts.SkillsName);
    public bool AutoDetect { get; init; } = true;
}



