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
                  "llm": {
                    "baseUrl": "http://localhost:8080",
                    "modelName": "local-model",
                    "maxTokens": 4096,
                    "temperature": 0.2,
                    "contextWindow": 8192
                  },
                  "agent": {
                    "maxIterations": 20,
                    "confirmWrite": true,
                    "confirmBash": true,
                    "allowedPaths": []
                  },
                  "skills": {
                    "autoDetect": true
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
