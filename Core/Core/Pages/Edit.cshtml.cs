using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Pages;

public class EditModel : PageModel
{
    private readonly DataContext _dataContext;
    public Product? Product { get; set; }

    public EditModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task OnGetAsync(long id)
    {
        Product = await _dataContext.Products.FindAsync(id);
    }

    public async Task<IActionResult> OnPostAsync(long id, decimal price)
    {
        var product = await _dataContext.Products.FindAsync(id);
        if (product != null) 
        {
            product.Price = price;
        }

        await _dataContext.SaveChangesAsync();
        
        return RedirectToPage();
    }
}

