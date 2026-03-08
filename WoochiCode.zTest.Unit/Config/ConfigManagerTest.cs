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
        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        try
        {
            var tempManager = new ConfigManager(tempDir);
            string result = tempManager.SaveDefaultConfig();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }
}
