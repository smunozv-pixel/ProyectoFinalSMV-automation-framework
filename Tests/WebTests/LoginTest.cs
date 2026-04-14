using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProyectoFinalSMV.Utilities;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace ProyectoFinalSMV.Tests.WebTests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
        }

        [Test]
        [Category("Web")]
        public void LoginValido()
        {
            var baseUrl = ConfigHelper.GetSetting("BaseUrl");
            var user = ConfigHelper.GetSetting("User");
            var password = ConfigHelper.GetSetting("Password");

            loginPage.NavigateToLoginPage(baseUrl);
            loginPage.Login(user, password);

            Assert.That(loginPage.IsLoginSuccessful(), Is.True,
                "El login no redirigió correctamente a la página de productos.");
        }

        [Test]
        [Category("Web")]
        public void LoginInvalido()
        {
            var baseUrl = ConfigHelper.GetSetting("BaseUrl");
            loginPage.NavigateToLoginPage(baseUrl);
            loginPage.Login("locked_out_user", "secret_sauce");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(d => loginPage.IsErrorMessageDisplayed());

            Assert.That(loginPage.IsErrorMessageDisplayed(),
                "No se mostró el mensaje de error en login inválido.");
            Assert.That(loginPage.GetErrorMessageText(),
                Does.Contain("locked out"),
                "El mensaje de error no corresponde al usuario bloqueado.");
        }

        [TearDown]
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

            
                    var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
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
}