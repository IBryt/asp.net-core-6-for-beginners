using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Core.Pages;

public class HandlerSelectorModel : PageModel
{
    private readonly DataContext _dataContext;
    public Product? Product { get; set; }

    public HandlerSelectorModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task OnGetAsync(long id = 1)
    {
        Product = await _dataContext.Products.FindAsync(id);
    }

    public async Task OnGetCategoryAsync(long id = 1)
    {
        Product = await _dataContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
