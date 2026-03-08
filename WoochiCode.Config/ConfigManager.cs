using System.Text.Json;
using WoochiCode.Core;
using WoochiCode.Core.Utils;
using WoochiCode.Core.Vo;

namespace WoochiCode.Config;

public class ConfigManager
{
    private readonly string _Cwd;

    public ConfigManager(string cwd)
    {
        _Cwd = cwd;
    }

    public AppConfig LoadConfig()
    {
        var configPath = PathUtils.GetProjectConfigFileAbsolutePath(_Cwd);

        if (!File.Exists(configPath))
            return BuildDefault(_Cwd);

        try
        {
            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<AppConfig>(json, AppConstracts.JsonOpts);

            if (config is null) return BuildDefault(_Cwd);

            if (config.Agent.AllowedPaths.Count == 0)
                config = config with { Agent = config.Agent with { AllowedPaths = [_Cwd] } };

            return config;
        }
        catch
        {
            return BuildDefault(_Cwd);
        }
    }

    public string SaveDefaultConfig()
    {
        string path = PathUtils.GetProjectConfigFileAbsolutePath(_Cwd);

        Directory.CreateDirectory(path);

        AppConfig config = new();

        File.WriteAllText(path, JsonSerializer.Serialize(config, AppConstracts.JsonOpts));

        Directory.CreateDirectory(Path.Combine(path, AppConstracts.SkillsName));
        Directory.CreateDirectory(Path.Combine(path, AppConstracts.HooksName));

        return path;
    }

    private AppConfig BuildDefault(string cwd) => new()
    {
        Agent = new AgentConfig { AllowedPaths = [cwd] },
    };
}
