using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Components;

public class ProductListingViewComponent : ViewComponent
{
    private DataContext _dataContext;

    public IEnumerable<Product> Products;

    public ProductListingViewComponent(DataContext context)
    {
        _dataContext = context;
    }

    public IViewComponentResult Invoke()
    {
        return View(_dataContext.Products.Include(p => p.Category).ToList());
    }
}