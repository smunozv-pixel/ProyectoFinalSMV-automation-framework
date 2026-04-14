using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using ProyectoFinalSMV.Utilities;
using Reqnroll;

[Binding]
public class Hooks(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private IWebDriver? driver;

    [BeforeScenario]
    public void Setup()
    {
        driver = DriverFactory.CreateDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        _scenarioContext["WebDriver"] = driver;
    }

    [AfterScenario]
    public void TearDown()
    {
        if (driver != null)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                
                var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                var screenshotsDir = Path.Combine(projectRoot, "Screenshots");

                if (!Directory.Exists(screenshotsDir))
                {
                    Directory.CreateDirectory(screenshotsDir);
                }

                
                var rawName = TestContext.CurrentContext.Test.Name;
                var safeName = Regex.Replace(rawName, @"[^a-zA-Z0-9_-]", "_");

                
                var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 6);

                var fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}_{uniqueId}.png";
                var filePath = Path.Combine(screenshotsDir, fileName);

                
                File.WriteAllBytes(filePath, screenshot.AsByteArray);

            
                TestContext.AddTestAttachment(filePath, "Screenshot del test");

                Console.WriteLine($"Screenshot guardado y adjuntado: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar screenshot: {ex.Message}");
            }
            finally
            {
                driver.Quit();
                driver.Dispose();
                driver = null!;
            }
        }
    }
}
