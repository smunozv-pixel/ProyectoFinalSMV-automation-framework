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

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            // Espera implícita de 5 segundos
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
            productsPage = new ProductsPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);

            loginPage.LoginAsStandardUser();
            productsPage.ClearCart();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
        }

        //5.Completar checkout exitoso con datos válidos (data-driven con JSON).

        [Test]
        [Category("Web")]
        public void CheckoutExitoso_ConDatosValidos()
        {
            // Agregar producto
            productsPage.AddProductToCart("sauce-labs-backpack");
            productsPage.GoToCart();

            // Ir al checkout
            cartPage.Checkout();

            // Llenar formulario desde JSON
            checkoutPage.FillFormFromJson("checkoutData.json");
            checkoutPage.Continue();
            Assert.That(checkoutPage.IsSummaryDisplayed(), Is.True,
                "El resumen de checkout no se mostró correctamente.");

            // Finalizar compra
            checkoutPage.Finish();
            Assert.That(checkoutPage.IsOrderComplete(), Is.True,
                "La orden no se completó correctamente.");
        }

        //6.Intentar checkout con datos incompletos y validar error.

        [Test]
        [Category("Web")]
        public void CheckoutConDatosIncompletos()
        {
            productsPage.AddProductToCart("sauce-labs-backpack");
            productsPage.GoToCart();
            cartPage.Checkout();

            // Llenar solo nombre, dejar apellido vacío
            checkoutPage.FillForm("Silvia", "", "12345");
            checkoutPage.Continue();

            Assert.That(checkoutPage.IsErrorMessageDisplayed(), Is.True,
                "El sistema debería mostrar un error si los datos están incompletos.");
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                try
                {
                    // Captura siempre al final del test
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                    // Carpeta "Screenshots" en la raíz del proyecto
                    var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                    var screenshotsDir = Path.Combine(projectRoot, "Screenshots");

                    if (!Directory.Exists(screenshotsDir))
                    {
                        Directory.CreateDirectory(screenshotsDir);
                    }

                    // Nombre de archivo con timestamp
                    var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(screenshotsDir, fileName);

                    // Guardar como PNG
                    File.WriteAllBytes(filePath, screenshot.AsByteArray);

                    // Adjuntar al reporte de NUnit
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