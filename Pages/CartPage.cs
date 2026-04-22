using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class CartPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public CartPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
    }

    private IWebElement CheckoutButton => driver.FindElement(By.Id("checkout"));
    private IWebElement CartLink => driver.FindElement(By.ClassName("shopping_cart_link"));

    public void GoToCart()
    {
        CartLink.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("checkout")));
    }

    public void Checkout()
    {
        GoToCart(); // asegurarse de estar en el carrito
        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("checkout"))).Click();
    }


}
