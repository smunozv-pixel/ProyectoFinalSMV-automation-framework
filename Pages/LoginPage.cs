using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By usernameField = By.Id("user-name");
    private readonly By passwordField = By.Id("password");
    private readonly By loginButton = By.Id("login-button");
    private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
    }

    public void NavigateToLoginPage(string url)
    {
        driver.Navigate().GoToUrl(url);
        Console.WriteLine("Navigated to login page: " + url);
    }

    public void Login(string username, string password)
    {
        Console.WriteLine($"Intentando login con usuario: {username}");
        wait.Until(ExpectedConditions.ElementIsVisible(usernameField));
        driver.FindElement(usernameField).Clear();
        driver.FindElement(usernameField).SendKeys(username);

        driver.FindElement(passwordField).Clear();
        driver.FindElement(passwordField).SendKeys(password);

        driver.FindElement(loginButton).Click();
    }

    public void LoginAsStandardUser()
    {
        NavigateToLoginPage("https://www.saucedemo.com/");
        Login("standard_user", "secret_sauce");
    }

    public bool IsLoginSuccessful()
    {
        // Verifica si aparece el contenedor de inventario después del login
        return driver.FindElements(By.Id("inventory_container")).Count > 0;
    }


    public bool IsErrorMessageDisplayed()
    {
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(errorMessage)).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }


    public string GetErrorMessageText()
    {
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(errorMessage)).Text;
        }
        catch (WebDriverTimeoutException)
        {
            return string.Empty;
        }

    }
}
