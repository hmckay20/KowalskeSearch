using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
  private readonly KohlerScrapingService _scrapingService;

  public ProductsController(KohlerScrapingService scrapingService)
  {
    _scrapingService = scrapingService;
  }

  // Existing method for a single serial number
  [HttpGet("{serialNumber}")]
  public async Task<IActionResult> GetProductBySerialNumber(string serialNumber)
  {
    var productInfo = _scrapingService.GetProductInfoBySerialNumber(serialNumber);
    if (productInfo == null)
    {
      return NotFound("Product not found.");
    }

    return Ok(productInfo);
  }

  // New method to handle multiple serial numbers
  [HttpPost("batchSearch")]
  public async Task<IActionResult> GetProductsBySerialNumbers([FromBody] List<string> serialNumbers)
  {
    var productInfos = new List<ProductInfo>();

    foreach (var serialNumber in serialNumbers)
    {
      var productInfo = _scrapingService.GetProductInfoBySerialNumber(serialNumber);
      if (productInfo != null)
      {
        productInfos.Add(productInfo);
      }
      else
      {
        // Optionally handle the case where a serial number doesn't correspond to a product
      }
    }

    if (!productInfos.Any())
    {
      return NotFound("No products found.");
    }

    return Ok(productInfos);
  }
}
