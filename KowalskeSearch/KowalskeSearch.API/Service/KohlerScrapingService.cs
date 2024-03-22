using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

public class KohlerScrapingService
{
  private readonly HttpClient _httpClient;

  public KohlerScrapingService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<ProductInfo> GetProductInfoBySerialNumber(string serialNumber)
  {
    var url = $"https://www.kohler.com/en/search?keyword={serialNumber}";
    var html = await _httpClient.GetStringAsync(url);
    var doc = new HtmlDocument();
    doc.LoadHtml(html);

    var xpathQuery = "//div[contains(@class, 'product-listing__tile-col') and contains(@class, 'product-listing__tile-col--full-width')]";
    var div = doc.DocumentNode.SelectSingleNode(xpathQuery);

    if (div != null)
    {
      // Extract name and price from the div. Adjust these based on the actual content structure.
      var name = div.SelectSingleNode(".//h2").InnerText.Trim(); // Example path
      var price = div.SelectSingleNode(".//span[contains(@class, 'price')]")?.InnerText.Trim(); // Example path

      return new ProductInfo
      {
        Name = name,
        Price = price
      };
    }

    return null;
  }
}
