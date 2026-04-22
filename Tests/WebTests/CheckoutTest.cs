using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using ProyectoFinalSMV.Utilities;

namespace ProyectoFinalSMV.Tests.WebTests
{
    [TestFixture]
    public class CheckoutTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private ProductsPage productsPage;
        private CartPage cartPage;
        private CheckoutPage checkoutPage;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ReportManager.InitReport();
        }

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
            productsPage = new ProductsPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);

            loginPage.LoginAsStandardUser();
            productsPage.ClearCart();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
        }

        [Test]
        [Category("Web")]
        public void CheckoutExitoso_ConDatosValidos()
        {
            ReportManager.CreateTest("Checkout Exitoso con datos válidos");

            ReportManager.LogInfo("Agrego producto al carrito");
            productsPage.AddProductToCart("sauce-labs-backpack");
            productsPage.GoToCart();

            ReportManager.LogInfo("Inicio proceso de checkout");
            cartPage.Checkout();

            ReportManager.LogInfo("Lleno formulario desde JSON");
            checkoutPage.FillFormFromJson("checkoutData.json");
            checkoutPage.Continue();
            Assert.That(checkoutPage.IsSummaryDisplayed(), Is.True,
                "El resumen de checkout no se mostró correctamente.");
            ReportManager.LogPass("Resumen de checkout mostrado correctamente");

            ReportManager.LogInfo("Finalizo compra");
            checkoutPage.Finish();
            Assert.That(checkoutPage.IsOrderComplete(), Is.True,
                "La orden no se completó correctamente.");
            ReportManager.LogPass("Orden completada exitosamente");
        }

        [Test]
        [Category("Web")]
        public void CheckoutConDatosIncompletos()
        {
            ReportManager.CreateTest("Checkout con datos incompletos");

            productsPage.AddProductToCart("sauce-labs-backpack");
            productsPage.GoToCart();
            cartPage.Checkout();

            ReportManager.LogInfo("Lleno formulario con datos incompletos");
            checkoutPage.FillForm("Silvia", "", "12345");
            checkoutPage.Continue();

            Assert.That(checkoutPage.IsErrorMessageDisplayed(), Is.True,
                "El sistema debería mostrar un error si los datos están incompletos.");
            ReportManager.LogPass("Error mostrado correctamente por datos incompletos");
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
                        Directory.CreateDirectory(screenshotsDir);

                    var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(screenshotsDir, fileName);

                    File.WriteAllBytes(filePath, screenshot.AsByteArray);
                    TestContext.AddTestAttachment(filePath, "Screenshot del test");

                    // Adjuntar al reporte si hubo fallo
                    if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                    {
                        ReportManager.LogFail("Test fallido")?.AddScreenCaptureFromPath(filePath);
                    }
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

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ReportManager.FlushReport();
        }
    }
}
