using WoochiCode.Core.Utils;

namespace WoochiCode.zTest.Unit.Core.Utils;

public class PathUtilsTest
{
    [Theory]
    [InlineData(new string[] { "skills" }, @"C:\Users\wjdanr\.config\woochi-code\skills")]
    [InlineData(new string[] { "a", "b", "c" }, @"C:\Users\wjdanr\.config\woochi-code\a\b\c")]
    [InlineData(new string[] { }, @"C:\Users\wjdanr\.config\woochi-code")]
    public void GetGlobalConfigDirAbsolutePath(string[] paths, string expected)
    {
        string result = PathUtils.GetGlobalConfigDirAbsolutePath(paths);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetProjectConfigDirAbsolutePath()
    {
        string cwd = ".";

        string expected = @".\.woochi-code";
        string result = PathUtils.GetProjectConfigDirAbsolutePath(cwd);

        Assert.Equal(expected, result);
    }
}
