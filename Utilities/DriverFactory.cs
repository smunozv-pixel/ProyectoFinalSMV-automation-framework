using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();

      
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

      
            options.AddArgument("--disable-save-password-bubble");

          
            options.AddArgument("--incognito");

          
            options.AddArgument("--start-maximized");

 
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            return driver;
        }
    }
