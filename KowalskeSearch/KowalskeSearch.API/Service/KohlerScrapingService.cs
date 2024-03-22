using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

public class KohlerScrapingService
{
  public KohlerScrapingService()
  {
    new DriverManager().SetUpDriver(new ChromeConfig());
  }

  public ProductInfo GetProductInfoBySerialNumber(string serialNumber)
  {
    var options = new ChromeOptions();
    options.AddArguments("headless"); // Run Chrome in headless mode.

    using (var driver = new ChromeDriver(options))
    {
      try
      {
        var url = $"https://www.kohler.com/en/search?keyword={serialNumber}";
        driver.Navigate().GoToUrl(url);

        // Example: new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(...);
        var element = driver.FindElement(By.CssSelector($"[id='Inner-{serialNumber}'][aria-label]")); // Corrected to use CSS Selector
        if (element != null)
        {
          var ariaLabel = element.GetAttribute("aria-label"); // Corrected variable name

          var parts = ariaLabel.Split(new string[] { ", Description: ", ", Sale Price: ", " Original Price: " }, StringSplitOptions.None);
          if (parts.Length > 3) // Corrected property name
          {
            return new ProductInfo
            {
              Name = parts[0].Trim()??"idk",
              Description = parts[1].Trim()??"idk",
              SalePrice = parts[2].Split(' ')[1].Trim()??"idk",
              OriginalPrice = parts[3].Split('.')[0].Trim()??"idk"
            };
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
      finally
      {
        driver.Quit();
      }
    }

    return null; // Return null or a default instance as needed.
  }
}
