using RestSharp;

public static class EvidenceHelper
{
    private static string GetProjectRoot()
    {
        // Sube tres niveles desde bin/Debug/net10.0 hasta la raíz del proyecto
        return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
    }

    public static void SaveJson(RestResponse response, string testName)
    {
        var folder = Path.Combine(GetProjectRoot(), "Evidence", "Json");
        Directory.CreateDirectory(folder);
        var filePath = Path.Combine(folder, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.json");
        File.WriteAllText(filePath, response.Content ?? string.Empty);
    }

    public static void SaveStatus(RestResponse response, string testName)
    {
        var folder = Path.Combine(GetProjectRoot(), "Evidence", "Status");
        Directory.CreateDirectory(folder);
        var filePath = Path.Combine(folder, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        File.WriteAllText(filePath, $"StatusCode: {response.StatusCode}");
    }
}
