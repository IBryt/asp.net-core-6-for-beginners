using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    [Produces("application/json", "application/xml")]
    public async Task<IActionResult> GetCategory(long id)
    {
        var category = await _dataContext.Categories
            .Include(c => c.Products)
            .FirstAsync(x => x.Id == id);

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

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchCategory(long id, JsonPatchDocument<Category> patchDoc)
    {
        var category = await _dataContext.Categories.FindAsync(id);

        if (category == null)
        {
            return BadRequest();
        }

        patchDoc.ApplyTo(category);
        await _dataContext.SaveChangesAsync();

        return Ok(category);
    }
}
