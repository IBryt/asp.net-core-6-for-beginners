using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly DataContext _dataContext;

    public ProductsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public IAsyncEnumerable<Product> GetProducts()
    {
        return _dataContext.Products.AsAsyncEnumerable();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(long id, [FromServices] ILogger<ProductsController> logger)
    {
        logger.LogDebug("GetProductById Action Invoked");
        var product = await _dataContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> SaveProduct(Product product)
    {
        await _dataContext.Products.AddAsync(product);
        await _dataContext.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPut]
    public void UpdateProduct(Product product)
    {
        _dataContext.Update(product);
        _dataContext.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void DeleteProduct(long id)
    {
        _dataContext.Products.Remove(new Product { Id = id });
        _dataContext.SaveChanges();
    }

    [HttpGet("redirect")]
    public IActionResult Redirect()
    {
        return RedirectToAction(nameof(GetProductById), "Products", new { Id = 1 });
    }
}
