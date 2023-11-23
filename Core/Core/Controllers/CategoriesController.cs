using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : Controller
{
    private readonly DataContext _dataContext;

    public CategoriesController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(long id)
    {
        var category = await _dataContext.Categories.Include(c => c.Products).FirstAsync(x => x.Id == id);
        if (category == null) 
        {
            return BadRequest();
        }

        foreach (var product in category.Products) 
        {
            product.Category = null;
        }

        return Ok(category);
    }
}
