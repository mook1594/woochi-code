using WoochiCode.Core.Logger;

namespace WoochiCode.zTest.Unit.Logger;

public class LoggerTest
{
    [Fact]
    public void Print()
    {
        ConsoleLogger.Instance.Info(LogCategory.Message, "test");
    }
}
