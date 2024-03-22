using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class KohlerScrapingService
{
  private readonly IHttpClientFactory _clientFactory;

  public KohlerScrapingService(IHttpClientFactory clientFactory)
  {
    _clientFactory = clientFactory;
  }

  public async Task<ProductInfo> GetProductInfoBySerialNumber(string serialNumber)
  {
    try
    {
      var productInfo = new ProductInfo();
      // Create a HttpClient instance from the factory
      var httpClient = _clientFactory.CreateClient();

      // Set the User-Agent and Accept headers to mimic a browser request
      httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
      httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

      // Construct the URL with the serial number, ensuring to replace {serialNumber} with the actual value
      var url = $"https://www.kohler.com/en/search?keyword={serialNumber}";

      // Create a HttpRequestMessage to set specific request headers like Cookies
      var request = new HttpRequestMessage(HttpMethod.Get, url);

      // Adding cookies to the request
      request.Headers.Add("Cookie", "name=value; anothername=anothervalue");

      // Sending the HTTP GET request using SendAsync instead of GetAsync for HttpRequestMessage
      var response = await httpClient.SendAsync(request);
      //Console.WriteLine(response.ToString());

      if (response.IsSuccessStatusCode)
      {
        var html = await response.Content.ReadAsStringAsync();
        var doc = new HtmlDocument();
       // Console.WriteLine(html);
        doc.LoadHtml(html);

        // XPath query to locate the desired div containing product information
        var xpathQuery = "//div[@id='Inner-K-29777-PA-0']";
;

        var div = doc.DocumentNode.SelectSingleNode(xpathQuery);
        if(div == null)
        {
          Console.WriteLine("I do not exist");
        }
        if (div != null)
        {
          var ariaLabel = div.GetAttributeValue("aria-label", string.Empty);

          productInfo.Name = ariaLabel;



          Console.WriteLine($"Product Name: {ariaLabel}");
          return new ProductInfo
          {
            Name = productInfo.Name,
            Price = "nothing"
          };
        }
      }

      return null;
    }
    catch (Exception ex)
    {
      // Log the exception or handle it as needed
      Console.WriteLine($"An error occurred: {ex.Message}");
      return null; // You might choose to return a more specific error message or object
    }
  }
}
