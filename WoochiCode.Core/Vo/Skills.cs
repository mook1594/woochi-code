namespace WoochiCode.Core.Vo;

public record SkillMeta
{
    public string Name { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public bool Always { get; set; }
    public string? Description { get; set; }
}

public record Skill
{
    public SkillMeta Meta { get; init; } = new();
    public string Content { get; init; } = string.Empty;
    public string FilePath { get; init; } = string.Empty;
}