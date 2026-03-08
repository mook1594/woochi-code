using System.Text.Json;
using WoochiCode.Core;
using WoochiCode.Core.Vo;

namespace WoochiCode.zTest.Unit.Core.Vo
{
    public class AppConfigTest
    {
        [Fact]
        public void DefaultValueTest()
        {
            string expected =
                """
                {
                  "Llm": {
                    "BaseUrl": "http://localhost:8080",
                    "ModelName": "local-model",
                    "MaxTokens": 4096,
                    "Temperature": 0.2,
                    "ContextWindow": 8192
                  },
                  "Agent": {
                    "MaxIterations": 20,
                    "ConfirmWrite": true,
                    "ConfirmBash": true,
                    "AllowedPaths": []
                  },
                  "Skills": {
                    "AutoDetect": true
                  }
                }
                """;

            AppConfig config = new();
            string json = JsonSerializer.Serialize(config, AppConstracts.JsonOpts);

            Console.WriteLine(json);

            Assert.Equal(expected, json);
        }
    }
}
