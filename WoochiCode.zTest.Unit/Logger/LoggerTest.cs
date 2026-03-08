using WoochiCode.Core.Logger;
namespace WoochiCode.zTest.Unit.Logger;

public class LoggerTest
{
    private ITestOutputHelper _Output;
    public LoggerTest(ITestOutputHelper output)
    {
        _Output = output;
    }

    [Fact]
    public void Print()
    {

        ConsoleLogger.Instance.Info(LogCategory.Message, "test");
        ConsoleLogger.Instance.Info(LogCategory.Tool, "test");
        ConsoleLogger.Instance.Info(LogCategory.Hook, "test");
        ConsoleLogger.Instance.Info(LogCategory.Skill, "test");
        ConsoleLogger.Instance.Info(LogCategory.Agent, "test");

        ConsoleLogger.Instance.Warn(LogCategory.Message, "test");
        ConsoleLogger.Instance.Warn(LogCategory.Tool, "test");
        ConsoleLogger.Instance.Warn(LogCategory.Hook, "test");
        ConsoleLogger.Instance.Warn(LogCategory.Skill, "test");
        ConsoleLogger.Instance.Warn(LogCategory.Agent, "test");

        ConsoleLogger.Instance.Error(LogCategory.Message, "test");
        ConsoleLogger.Instance.Error(LogCategory.Tool, "test");
        ConsoleLogger.Instance.Error(LogCategory.Hook, "test");
        ConsoleLogger.Instance.Error(LogCategory.Skill, "test");
        ConsoleLogger.Instance.Error(LogCategory.Agent, "test");

    }

    [Fact(Skip = "")]
    public void Print1()
    {
        var originalOut = Console.Out;
        string expected =
            """
            test
            { a = 1 }
            
            """;

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        ConsoleLogger.Instance.Info(LogCategory.Message, "test", new { a = "1" });

        var output = stringWriter.ToString();

        Console.SetOut(originalOut);

        string actual = output.Split("]: ")[1];

        _Output.WriteLine(expected);
        _Output.WriteLine(actual);

        Assert.NotEmpty(output);
        Assert.Equal(expected, actual);
    }

    [Fact(Skip = "")]
    public void Print2()
    {
        var originalOut = Console.Out;
        string expected =
            """
            test
            test
            
            """;

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        ConsoleLogger.Instance.Info(LogCategory.Message, "test", "test");

        var output = stringWriter.ToString();

        Console.SetOut(originalOut);

        string actual = output.Split("]: ")[1];

        _Output.WriteLine(expected);
        _Output.WriteLine(actual);

        Assert.NotEmpty(output);
        Assert.Equal(expected, actual);

    }

}