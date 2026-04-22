<<<<<<< HEAD
﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using ProyectoFinalSMV.Utilities;
using Reqnroll;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;
    private IWebDriver? driver;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        ReportManager.InitReport();
    }

    [BeforeScenario]
    public void Setup()
    {
        driver = DriverFactory.CreateDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        _scenarioContext["WebDriver"] = driver;

        // Crear test en el reporte con el nombre del escenario
        ReportManager.CreateTest(_scenarioContext.ScenarioInfo.Title);
        ReportManager.LogInfo($"Inicio del escenario: {_scenarioContext.ScenarioInfo.Title}");
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
                    Directory.CreateDirectory(screenshotsDir);

                var rawName = TestContext.CurrentContext.Test.Name;
                var safeName = Regex.Replace(rawName, @"[^a-zA-Z0-9_-]", "_");
                var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 6);

                var fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}_{uniqueId}.png";
                var filePath = Path.Combine(screenshotsDir, fileName);

                File.WriteAllBytes(filePath, screenshot.AsByteArray);
                TestContext.AddTestAttachment(filePath, "Screenshot del escenario");

                // Adjuntar al reporte si hubo fallo
                if (_scenarioContext.TestError != null)
                {
                    ReportManager.LogFail("Escenario fallido")?.AddScreenCaptureFromPath(filePath);
                }
                else
                {
                    ReportManager.LogPass("Escenario completado exitosamente");
                }

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

    [AfterTestRun]
    public static void AfterTestRun()
    {
        ReportManager.FlushReport();
    }
}
=======
﻿using System;
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
>>>>>>> e8c7dd8bb42ff9b5a23c779e0e0029d116e3d4a4
