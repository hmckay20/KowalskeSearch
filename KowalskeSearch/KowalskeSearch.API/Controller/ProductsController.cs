using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
  private readonly KohlerScrapingService _scrapingService;

  public ProductsController(KohlerScrapingService scrapingService)
  {
    _scrapingService = scrapingService;
  }

  [HttpGet("{serialNumber}")]
  public async Task<IActionResult> GetProductBySerialNumber(string serialNumber)
  {
    var productInfo = await _scrapingService.GetProductInfoBySerialNumber(serialNumber);
    if (productInfo == null)
      return NotFound("Product not found.");

    return Ok(productInfo);
  }
}
