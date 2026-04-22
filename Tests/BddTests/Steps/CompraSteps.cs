using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using ProyectoFinalSMV.Utilities;

[Binding]
public class CompraSteps
{
    private readonly IWebDriver driver;
    private LoginPage loginPage = null!;
    private ProductsPage productsPage = null!;
    private CartPage cartPage = null!;
    private CheckoutPage checkoutPage = null!;

    public CompraSteps(ScenarioContext scenarioContext)
    {
        driver = scenarioContext["WebDriver"] as IWebDriver
                 ?? throw new InvalidOperationException("WebDriver no inicializado en ScenarioContext");
    }

    [Given(@"que estoy en la página de login")]
    public void DadoQueEstoyEnLaPaginaDeLogin()
    {
        loginPage = new LoginPage(driver);
        loginPage.NavigateToLoginPage("https://www.saucedemo.com/");
        ReportManager.LogInfo("Navego a la página de login");
    }

    [When(@"inicio sesión con usuario ""(.*)"" y contraseña ""(.*)""")]
    public void CuandoInicioSesion(string usuario, string contraseña)
    {
        productsPage = new ProductsPage(driver);
        loginPage.Login(usuario, contraseña);
        ReportManager.LogInfo($"Inicio sesión con usuario: {usuario}");
    }

    [When(@"agrego el producto ""(.*)"" al carrito")]
    public void CuandoAgregoProducto(string productoNombre)
    {
        string productId = productoNombre switch
        {
            "Sauce Labs Backpack" => "sauce-labs-backpack",
            "Sauce Labs Bike Light" => "sauce-labs-bike-light",
            "Sauce Labs Bolt T-Shirt" => "sauce-labs-bolt-t-shirt",
            "Sauce Labs Fleece Jacket" => "sauce-labs-fleece-jacket",
            "Sauce Labs Onesie" => "sauce-labs-onesie",
            "Test.allTheThings() T-Shirt (Red)" => "test.allthethings()-t-shirt-(red)",
            _ => throw new ArgumentException($"Producto no soportado: {productoNombre}")
        };

        productsPage.AddProductToCart(productId);
        cartPage = new CartPage(driver);
        ReportManager.LogInfo($"Agrego producto al carrito: {productoNombre}");
    }

    [When(@"procedo al checkout con datos ""(.*)"", ""(.*)"", ""(.*)""")]
    public void CuandoProcedoCheckout(string nombre, string apellido, string codigoPostal)
    {
        cartPage.Checkout();
        checkoutPage = new CheckoutPage(driver);

        checkoutPage.FillForm(nombre, apellido, codigoPostal);
        checkoutPage.Continue();
        Assert.That(checkoutPage.IsSummaryDisplayed(), Is.True, "El resumen no se mostró correctamente");
        ReportManager.LogPass("Resumen de checkout mostrado correctamente");

        checkoutPage.Finish();
        ReportManager.LogInfo("Finalizo la compra");
    }

    [Then(@"debería ver el mensaje de confirmación ""(.*)""")]
    public void EntoncesDeberiaVerMensaje(string mensajeEsperado)
    {
        Assert.That(checkoutPage.IsOrderComplete(), Is.True, "La orden no se completó correctamente");
        ReportManager.LogPass($"Mensaje de confirmación mostrado: {mensajeEsperado}");
    }
}
