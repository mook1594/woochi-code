using System.Text.Json;
using WoochiCode.Core;
using WoochiCode.Core.Logger;
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

    /// <summary>
    /// 프로젝트 Config 로드
    /// </summary>
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

    /// <summary>
    /// Config 기본 값 저장
    /// </summary>
    public string SaveDefaultConfig()
    {
        string dirPath = PathUtils.GetProjectConfigDirAbsolutePath(_Cwd);
        ConsoleLogger.Instance.Info(LogCategory.Message, "default config 저장 경로", dirPath);

        Directory.CreateDirectory(dirPath);

        AppConfig config = new();

        string filePath = PathUtils.GetProjectConfigFileAbsolutePath(_Cwd);
        File.WriteAllText(filePath, JsonSerializer.Serialize(config, AppConstracts.JsonOpts));

        Directory.CreateDirectory(Path.Combine(dirPath, AppConstracts.SkillsName));
        Directory.CreateDirectory(Path.Combine(dirPath, AppConstracts.HooksName));

        return dirPath;
    }

    private AppConfig BuildDefault(string cwd) => new()
    {
        Agent = new AgentConfig { AllowedPaths = [cwd] },
    };
}
