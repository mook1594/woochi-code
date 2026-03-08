using System.Text.Json;

namespace WoochiCode.Core;

public class AppConstracts
{
    public const string ConfigName = "config";
    public const string AppName = "woochi-code";
    public const string SkillsName = "skills";
    public const string HooksName = "hooks";

    public static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}


