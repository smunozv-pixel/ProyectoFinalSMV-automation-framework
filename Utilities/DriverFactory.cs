using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();


            // Desactiva el gestor de contraseñas y el aviso de contraseñas inseguras
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            // Evita la burbuja de "guardar contraseña"
            options.AddArgument("--disable-save-password-bubble");

            // Opcional: usar modo incógnito para un perfil limpio
            options.AddArgument("--incognito");

            // Opcional: maximizar ventana al iniciar
            options.AddArgument("--start-maximized");

            // Inicializa el driver con las opciones configuradas

      
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
