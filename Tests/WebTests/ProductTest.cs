using OpenQA.Selenium;
using NUnit.Framework;
using ProyectoFinalSMV.Utilities;

namespace ProyectoFinalSMV.Tests.WebTests
{
    [TestFixture]
    public class ProductsTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private ProductsPage productsPage;

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
            loginPage.LoginAsStandardUser();
            productsPage.ClearCart();
        }

        [Test]
        [Category("Web")]
        public void AddProductsToCar()
        {
            ReportManager.CreateTest("Agregar productos al carrito");

            ReportManager.LogInfo("Agrego 3 productos al carrito");
            productsPage.AddProductToCart("sauce-labs-backpack");
            productsPage.AddProductToCart("sauce-labs-bike-light");
            productsPage.AddProductToCart("sauce-labs-bolt-t-shirt");

            Assert.That(productsPage.GetCartCount(), Is.EqualTo(3),
                "El carrito debería tener 3 productos después de agregarlos.");
            ReportManager.LogPass("Carrito contiene 3 productos correctamente");

            productsPage.GoToCart();
        }

        [Test]
        [Category("Web")]
        public void RemoveProductsToCar()
        {
            ReportManager.CreateTest("Eliminar productos del carrito");

            productsPage.AddProductToCart("sauce-labs-backpack");
            Assert.That(productsPage.GetCartCount(), Is.EqualTo(1));
            ReportManager.LogInfo("Producto agregado al carrito");

            productsPage.RemoveProductFromCart("sauce-labs-backpack");
            Assert.That(productsPage.GetCartCount(), Is.EqualTo(0));
            ReportManager.LogPass("Producto eliminado correctamente");
        }

        [Test]
        [Category("Web")]
        public void ValidarOrdenamientoPorPrecio()
        {
            ReportManager.CreateTest("Validar ordenamiento por precio");

            productsPage.SelectSortOption("Price (low to high)");
            var precios = productsPage.GetAllProductPrices();

            Assert.That(precios, Is.Ordered,
                "Los productos no están ordenados correctamente por precio ascendente.");
            ReportManager.LogPass("Productos ordenados correctamente por precio ascendente");
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
