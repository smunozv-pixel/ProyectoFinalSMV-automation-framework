<<<<<<< HEAD
﻿using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

public class ProductsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private By cartIcon = By.Id("shopping_cart_container");
    private By cartBadge = By.ClassName("shopping_cart_badge");

    public ProductsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
    }

    public bool IsVisible()
    {
        try
        {
            bool visible = driver.FindElement(By.Id("inventory_container")).Displayed;
            Console.WriteLine("Inventory container visible: " + visible);
            return visible;
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Inventory container not found.");
            return false;
        }
    }

    public void AddProductToCart(string productId)
    {
        var addButtonId = $"add-to-cart-{productId}";
        var removeButtonId = $"remove-{productId}";

        // Verifica si existe el botón Add
        var addButtons = driver.FindElements(By.Id(addButtonId));
        if (addButtons.Any())
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(addButtonId))).Click();
            Console.WriteLine("Clicked " + addButtonId);
        }
        else
        {
            // Si no existe Add, puede que ya esté en el carrito
            var removeButtons = driver.FindElements(By.Id(removeButtonId));
            if (removeButtons.Any())
            {
                Console.WriteLine($"Producto {productId} ya estaba en el carrito (botón Remove visible).");
            }
            else
            {
                throw new NoSuchElementException($"No se encontró el botón Add ni Remove para {productId}");
            }
        }
    }

    public void RemoveProductFromCart(string productId)
    {
        int initialCount = GetCartCount();

        var removeButton = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.Id($"remove-{productId}")
        ));
        removeButton.Click();
        Console.WriteLine($"Clicked remove-{productId}");

        wait.Until(d =>
        {
            int currentCount = GetCartCount();
            return currentCount < initialCount;
        });

        Console.WriteLine("Cart badge after remove: " + GetCartCount());
    }

    public void ClearCart()
    {
        // Ir al carrito
        GoToCart();

        // Esperar a que cargue el contenedor del carrito (aunque esté vacío)
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cart_contents_container")));

        // Buscar botones "Remove"
        var removeButtons = driver.FindElements(By.XPath("//button[text()='Remove']"));
        if (removeButtons.Count == 0)
        {
            Console.WriteLine("Carrito ya estaba vacío.");
        }
        else
        {
            foreach (var btn in removeButtons)
            {
                btn.Click();
                Console.WriteLine("Clicked " + btn.Text);
            }

            // Esperar hasta que el badge desaparezca o sea 0
            wait.Until(d =>
            {
                var elements = d.FindElements(cartBadge);
                return elements.Count == 0 || elements[0].Text == "0";
            });

            Console.WriteLine("Carrito vaciado. GetCartCount() -> " + GetCartCount());
        }

        // Volver al inventario
        driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
    }

    public int GetCartCount()
    {
        var elements = driver.FindElements(cartBadge);
        int count = elements.Count > 0 ? int.Parse(elements[0].Text) : 0;
        Console.WriteLine("GetCartCount() -> " + count);
        return count;
    }

    public int GetCartItemsCount()
    {
        GoToCart();
        var items = driver.FindElements(By.CssSelector(".cart_item .inventory_item_name"));
        Console.WriteLine("Items in cart page: " + items.Count);
        return items.Count;
    }

    public bool IsProductInCart(string productName)
    {
        GoToCart();
        var items = driver.FindElements(By.CssSelector(".cart_item .inventory_item_name"));
        bool found = items.Any(i => i.Text.Equals(productName));
        Console.WriteLine($"IsProductInCart('{productName}') -> {found}");
        return found;
    }

    public void GoToCart()
    {
        var element = wait.Until(ExpectedConditions.ElementToBeClickable(cartIcon));
        element.Click();
        Console.WriteLine("Navigated to cart.");

        // Espera a que cargue el contenedor del carrito (aunque esté vacío)
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cart_contents_container")));
    }

    public void SelectSortOption(string optionText)
    {
        // El dropdown de ordenamiento en SauceDemo tiene id "product_sort_container"
        var sortDropdown = driver.FindElement(By.CssSelector("select.product_sort_container"));
        var selectElement = new SelectElement(sortDropdown);
        selectElement.SelectByText(optionText);
        Console.WriteLine($"Ordenamiento seleccionado: {optionText}");
    }

    public List<decimal> GetAllProductPrices()
    {
        // Los precios están en elementos con clase "inventory_item_price"
        var priceElements = driver.FindElements(By.CssSelector(".inventory_item_price"));
        var prices = priceElements
            .Select(e => Decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
            .ToList();

        Console.WriteLine("Precios obtenidos: " + string.Join(", ", prices));
        return prices;
    }
=======
﻿using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

public class ProductsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private By cartIcon = By.Id("shopping_cart_container");
    private By cartBadge = By.ClassName("shopping_cart_badge");

    public ProductsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
    }

    public bool IsVisible()
    {
        try
        {
            bool visible = driver.FindElement(By.Id("inventory_container")).Displayed;
            Console.WriteLine("Inventory container visible: " + visible);
            return visible;
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Inventory container not found.");
            return false;
        }
    }

    public void AddProductToCart(string productId)
    {
        var addButtonId = $"add-to-cart-{productId}";
        var removeButtonId = $"remove-{productId}";

     
        var addButtons = driver.FindElements(By.Id(addButtonId));
        if (addButtons.Any())
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(addButtonId))).Click();
            Console.WriteLine("Clicked " + addButtonId);
        }
        else
        {
            
            var removeButtons = driver.FindElements(By.Id(removeButtonId));
            if (removeButtons.Any())
            {
                Console.WriteLine($"Producto {productId} ya estaba en el carrito (botón Remove visible).");
            }
            else
            {
                throw new NoSuchElementException($"No se encontró el botón Add ni Remove para {productId}");
            }
        }
    }

    public void RemoveProductFromCart(string productId)
    {
        int initialCount = GetCartCount();

        var removeButton = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.Id($"remove-{productId}")
        ));
        removeButton.Click();
        Console.WriteLine($"Clicked remove-{productId}");

        wait.Until(d =>
        {
            int currentCount = GetCartCount();
            return currentCount < initialCount;
        });

        Console.WriteLine("Cart badge after remove: " + GetCartCount());
    }

    public void ClearCart()
    {
       
        GoToCart();

        
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cart_contents_container")));

        
        var removeButtons = driver.FindElements(By.XPath("//button[text()='Remove']"));
        if (removeButtons.Count == 0)
        {
            Console.WriteLine("Carrito ya estaba vacío.");
        }
        else
        {
            foreach (var btn in removeButtons)
            {
                btn.Click();
                Console.WriteLine("Clicked " + btn.Text);
            }

            wait.Until(d =>
            {
                var elements = d.FindElements(cartBadge);
                return elements.Count == 0 || elements[0].Text == "0";
            });

            Console.WriteLine("Carrito vaciado. GetCartCount() -> " + GetCartCount());
        }

    
        driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
    }

    public int GetCartCount()
    {
        var elements = driver.FindElements(cartBadge);
        int count = elements.Count > 0 ? int.Parse(elements[0].Text) : 0;
        Console.WriteLine("GetCartCount() -> " + count);
        return count;
    }

    public int GetCartItemsCount()
    {
        GoToCart();
        var items = driver.FindElements(By.CssSelector(".cart_item .inventory_item_name"));
        Console.WriteLine("Items in cart page: " + items.Count);
        return items.Count;
    }

    public bool IsProductInCart(string productName)
    {
        GoToCart();
        var items = driver.FindElements(By.CssSelector(".cart_item .inventory_item_name"));
        bool found = items.Any(i => i.Text.Equals(productName));
        Console.WriteLine($"IsProductInCart('{productName}') -> {found}");
        return found;
    }

    public void GoToCart()
    {
        var element = wait.Until(ExpectedConditions.ElementToBeClickable(cartIcon));
        element.Click();
        Console.WriteLine("Navigated to cart.");

       
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cart_contents_container")));
    }

    public void SelectSortOption(string optionText)
    {
        
        var sortDropdown = driver.FindElement(By.CssSelector("select.product_sort_container"));
        var selectElement = new SelectElement(sortDropdown);
        selectElement.SelectByText(optionText);
        Console.WriteLine($"Ordenamiento seleccionado: {optionText}");
    }

    public List<decimal> GetAllProductPrices()
    {
        
        var priceElements = driver.FindElements(By.CssSelector(".inventory_item_price"));
        var prices = priceElements
            .Select(e => Decimal.Parse(e.Text.Replace("$", ""), CultureInfo.InvariantCulture))
            .ToList();

        Console.WriteLine("Precios obtenidos: " + string.Join(", ", prices));
        return prices;
    }
>>>>>>> e8c7dd8bb42ff9b5a23c779e0e0029d116e3d4a4
}