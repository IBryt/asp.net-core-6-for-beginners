using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly DataContext _dataContext;

    public ProductsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        return  _dataContext.Products;
    }

    [HttpGet("{id}")]
    public Product GetProductById(long id,[FromServices]ILogger<ProductsController> logger)
    {
        logger.LogDebug("GetProductById Action Invoked");
        return _dataContext.Products.Find(id);
    }

    [HttpPost]
    public void  SaveProduct([FromBody] Product product)
    {
        _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
    }
}
