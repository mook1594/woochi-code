namespace WoochiCode.Core.Utils;

public class PathUtils
{
    private static string BaseGlobalPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    public static string GetGlobalConfigDirAbsolutePath(params string[] paths)
    {
        var basePath = Path.Combine(BaseGlobalPath, $".{AppConstracts.ConfigName}", AppConstracts.AppName);
        return paths.Length > 0 ? Path.Combine(basePath, Path.Combine(paths)) : basePath;
    }

    public static string GetProjectConfigDirAbsolutePath(string cwd)
    {
        return Path.Combine(cwd, $".{AppConstracts.AppName}");
    }

    public static string GetProjectConfigFileAbsolutePath(string cwd)
    {
        return Path.Combine(GetProjectConfigDirAbsolutePath(cwd), $"{AppConstracts.ConfigName}.json");
    }
}
