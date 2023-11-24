using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Pages;

public class IndexModel : PageModel
{
    private readonly DataContext _dataContext;
    public Product Product { get; set; }

    public IndexModel(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task OnGetAsync(long id = 1)
    {

        Product = await _dataContext.Products.FindAsync(id);
    }
}