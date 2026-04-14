using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json.Linq;

public class CheckoutPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

  
    private readonly By firstNameField = By.Id("first-name");
    private readonly By lastNameField = By.Id("last-name");
    private readonly By postalCodeField = By.Id("postal-code");
    private readonly By continueButton = By.Id("continue");
    private readonly By finishButton = By.Id("finish");

    private readonly By summaryTotalLabel = By.CssSelector(".summary_total_label");
    private readonly By orderCompleteHeader = By.CssSelector(".complete-header");

    public CheckoutPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
    }
    public void FillForm(string firstName, string lastName, string postalCode)
    {
        driver.FindElement(By.Id("first-name")).SendKeys(firstName);
        driver.FindElement(By.Id("last-name")).SendKeys(lastName);
        driver.FindElement(By.Id("postal-code")).SendKeys(postalCode);
    }


    public void FillFormFromJson(string jsonFilePath)
    {
        var jsonData = File.ReadAllText(Path.Combine("TestData", jsonFilePath));
        var checkoutData = JObject.Parse(jsonData);

        driver.FindElement(firstNameField).SendKeys(checkoutData["firstName"]?.ToString() ?? "");
        driver.FindElement(lastNameField).SendKeys(checkoutData["lastName"]?.ToString() ?? "");
        driver.FindElement(postalCodeField).SendKeys(checkoutData["postalCode"]?.ToString() ?? "");
    }

    public bool IsErrorMessageDisplayed()
    {
       
        var errors = driver.FindElements(By.CssSelector("h3[data-test='error']"));
        return errors.Any();
    }
    
    public void Continue()
    {
        driver.FindElement(continueButton).Click();
    }

    
    public bool IsSummaryDisplayed()
    {
        try
        {
            wait.Until(ExpectedConditions.ElementIsVisible(summaryTotalLabel));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }


    public bool GoToOverviewAndValidate()
    {
        Continue();
        Console.WriteLine("Checkout page reached, URL: " + driver.Url);
        return IsSummaryDisplayed();
    }


    public void Finish()
    {
        driver.FindElement(finishButton).Click();
    }


    public bool IsOrderComplete()
    {
        try
        {
            wait.Until(ExpectedConditions.ElementIsVisible(orderCompleteHeader));
            var text = driver.FindElement(orderCompleteHeader).Text;
            Console.WriteLine("Order complete header text: " + text);
            return text.Trim().ToUpper().Contains("THANK YOU FOR YOUR ORDER");
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    private readonly By errorMessage = By.CssSelector("h3[data-test='error']");


    public string GetCheckoutErrorMessage()
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
