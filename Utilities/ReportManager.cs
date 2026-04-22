using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

public static class ReportManager
{
    private static ExtentReports _extent = new ExtentReports();

    private static ExtentTest? _test;


    public static void InitReport()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        var reportsDir = Path.Combine(projectRoot, "Reports");

        if (!Directory.Exists(reportsDir))
        {
            Directory.CreateDirectory(reportsDir);
        }

        var reportPath = Path.Combine(reportsDir, "TestReport.html");
        var sparkReporter = new ExtentSparkReporter(reportPath);

        _extent.AttachReporter(sparkReporter);
    }

    public static void CreateTest(string testName)
    {
        _test = _extent.CreateTest(testName);
    }

    public static void LogInfo(string message) => _test?.Info(message);
    public static void LogPass(string message) => _test?.Pass(message);
    public static ExtentTest? LogFail(string message)
    {
        return _test?.Fail(message);
    }


    public static void FlushReport() => _extent.Flush();
}
