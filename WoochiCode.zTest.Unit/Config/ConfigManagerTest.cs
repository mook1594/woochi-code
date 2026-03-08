using WoochiCode.Config;
using WoochiCode.Core.Vo;

namespace WoochiCode.zTest.Unit.Config;

public class ConfigManagerTest
{
    private const string CWD = ".";
    private ConfigManager manager;

    public ConfigManagerTest()
    {
        manager = new(CWD);
    }

    [Fact]
    public void LoadConfig()
    {
        string cwd = ".";
        AppConfig expected = new()
        {
            Agent = new AgentConfig { AllowedPaths = [cwd] }
        };

        AppConfig config = manager.LoadConfig();

        Assert.Equivalent(expected, config);
    }

    [Fact]
    public void SaveDefaultConfig()
    {
        string expected = "";

        string result = manager.SaveDefaultConfig();
        Assert.Equal(expected, result);
    }
}
